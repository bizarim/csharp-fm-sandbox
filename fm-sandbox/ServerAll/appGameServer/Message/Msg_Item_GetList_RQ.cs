using Compress;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    /// <summary>
    /// 아이템 리스트 정보 요청
    /// </summary>
    public class Msg_Item_GetList_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Item_GetList_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            // 기본 영주 정보 얻어오기
            fmLord lord = null;
            m_session.TryGetLord(out lord);

            // 프로토콜 RQ
            using (var recvfmProtocol = new PT_CG_Item_GetList_RQ())
            {
                // 프로토콜 Read
                recvfmProtocol.Deserialize(m_recvPacket);

                // 프로토콜 RS
                using (var sendfmProtocol = new LZ4_PT_CG_Item_GetList_RS())
                {
                    // check
                    if (null == lord)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }
                    // check state
                    if (lord.State != eLordState.Normal)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    // 아이템 정보 얻기
                    lord.TryGetItems(sendfmProtocol);
                    // 프로토콜 전송
                    m_session.SendPacket(sendfmProtocol);
                }
            }
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;
            m_recvPacket = null;
        }
        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }
}

using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Linq;

namespace appGameServer
{
    /// <summary>
    /// 영주 기본 정보 요청
    /// </summary>
    public class Msg_Lord_GetLord_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Lord_GetLord_RQ(appServer server, SessionBase session, Packet packet)
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
            using (var recvfmProtocol = new PT_CG_Lord_GetLord_RQ())
            {
                // 프로토콜 Read
                recvfmProtocol.Deserialize(m_recvPacket);

                // 프로토콜 RS
                using (var sendfmProtocol = new PT_CG_Lord_GetLord_RS())
                {
                    if (null == lord)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    using (var query = new urq_GetOtherLord(eRedis.Game))
                    {
                        query.i_strName = recvfmProtocol.Name;
                        sendfmProtocol.m_eErrorCode = query.Execute();

                        if (sendfmProtocol.m_eErrorCode == eErrorCode.Success)
                        {
                            sendfmProtocol.Lv = query.o_lordBase.Lv;
                            sendfmProtocol.Items = query.o_items.Where(x => x.Equip == true).ToList();
                        }
                    }

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

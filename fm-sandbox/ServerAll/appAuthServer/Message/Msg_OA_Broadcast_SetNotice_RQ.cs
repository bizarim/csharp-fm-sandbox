using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 방송 노티 설정 RQ
    /// </summary>
    public class Msg_OA_Broadcast_SetNotice_RQ : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_OA_Broadcast_SetNotice_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_OA_Broadcast_SetNotice_RQ())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                using (var sendfmProtocol = new PT_OA_Broadcast_SetNotice_RS())
                {
                    AuthServer authSvr = m_server as AuthServer;
                    if (true == authSvr.SaveNotice(recvfmProtocol.m_eLanguage, recvfmProtocol.m_strContents))
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                        m_session.SendPacket(sendfmProtocol);
                    }
                    else
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.OP_FailedSetNotice;
                        m_session.SendPacket(sendfmProtocol);
                    }

                }
            }
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;

            m_recvPacket.Dispose();
            m_recvPacket = null;
        }
        public override void Exclude()
        {
            // 클라이언트 세션일때
            //m_session.Logout();
        }
    }
}



using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 방송 노티 RQ
    /// </summary>
    public class Msg_CA_Broadcast_GetNotice_RQ : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_CA_Broadcast_GetNotice_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_CA_Broadcast_GetNotice_RQ())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                using (var sendfmProtocol = new PT_CA_Broadcast_GetNotice_RS())
                {
                    AuthServer authsvr = m_server as AuthServer;
                    string noti = authsvr.GetNotice(recvfmProtocol.m_eLanguage);
                    if (true == string.IsNullOrEmpty(noti))
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                        sendfmProtocol.m_strContents = string.Empty;
                        m_session.SendPacket(sendfmProtocol);
                    }
                    else
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                        sendfmProtocol.m_strContents = noti;
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


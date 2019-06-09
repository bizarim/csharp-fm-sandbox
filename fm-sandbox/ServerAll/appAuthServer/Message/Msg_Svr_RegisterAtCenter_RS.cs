using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 서버 Center에 등록 RS
    /// </summary>
    public class Msg_Svr_RegisterAtCenter_RS : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_Svr_RegisterAtCenter_RS(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_Server_RegisterAtCenter_RS())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                if (eErrorCode.Success != recvfmProtocol.m_eErrorCode)
                {
                    Logger.Error("Fail PT_Server_RegisterAtCenter_RS");
                    
                    return;
                }

                using (var sendfmProtocol = new PT_Server_ReadyToStart_RQ())
                {
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
            // 클라이언트 세션일때
            //m_session.Logout();
        }
    }
}

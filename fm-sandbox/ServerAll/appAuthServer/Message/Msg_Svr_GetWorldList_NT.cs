using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 서버 리스트 notices
    /// </summary>
    public class Msg_Svr_GetWorldList_NT : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_Svr_GetWorldList_NT(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_CA_Server_GetWorldList_NT())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                if (eErrorCode.Success == recvfmProtocol.m_eErrorCode)
                {
                    AuthServer authsvr = m_server as AuthServer;
                    authsvr.m_managerWorld.TryAdd(recvfmProtocol.m_list.Clone());
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


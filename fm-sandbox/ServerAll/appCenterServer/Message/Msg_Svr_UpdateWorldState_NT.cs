using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 서버 상태 노티
    /// </summary>
    public class Msg_Svr_UpdateWorldState_NT : IMessage
    {
        ServerSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Svr_UpdateWorldState_NT(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ServerSession;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_Server_UpdateWorldState_NT())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                RegisteredServerManager.Instance.UpdateWorldState(recvfmProtocol.m_nSequence, recvfmProtocol.m_nPlayerCount);
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
        }
    }
}

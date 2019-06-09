using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 방송 설정 RQ
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
            RegisteredServerManager.Instance.TryBroadcastToAuthServer(m_recvPacket);
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

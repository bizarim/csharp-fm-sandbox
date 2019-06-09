using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    public class Msg_Shop_GetList_RQ : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_Shop_GetList_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
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

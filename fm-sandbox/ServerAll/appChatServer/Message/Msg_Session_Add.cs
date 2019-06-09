using fmLibrary;
using fmServerCommon;

namespace appChatServer.Message
{
    /// <summary>
    /// 클라이언트 세션 등록
    /// </summary>
    public class Msg_Session_Add : IMessage
    {
        ClientSession m_session = null;

        public Msg_Session_Add(appServer server, SessionBase session)
        {
            m_server = server;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            ClientSessionManager.Instance.Add(m_session);
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;
        }
        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }

    public class Msg_Session_Remove : IMessage
    {
        ClientSession m_session = null;

        public Msg_Session_Remove(appServer server, SessionBase session)
        {
            m_server = server;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            ClientSessionManager.Instance.Remove(m_session);
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;
        }
        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }
}

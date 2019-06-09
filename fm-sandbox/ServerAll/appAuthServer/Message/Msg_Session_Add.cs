using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 서버에 클라이언트 접속 처리
    /// </summary>
    public class Msg_Session_Add : IMessage
    {
        SessionBase m_session = null;
        SessionManager m_csm = null;

        public Msg_Session_Add(SessionManager csm, SessionBase session)
        {
            m_csm = csm;
            m_session = session;
        }

        public override void Process()
        {
            m_csm.TryAdd(m_session);
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
        SessionBase m_session = null;
        SessionManager m_csm = null;

        public Msg_Session_Remove(SessionManager csm, SessionBase session)
        {
            m_csm = csm;
            m_session = session;
        }

        public override void Process()
        {
            m_csm.Remove(m_session);
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

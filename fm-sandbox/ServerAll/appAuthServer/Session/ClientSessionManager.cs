using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 클라리언트 세션 관리자
    /// </summary>
    public class ClientSessionManager : SessionManager
    {
        private SessionContainer[] m_container;
        private readonly int MaxCount = 10;

        protected int Get(long id) { return (int)(id % MaxCount); }

        public ClientSessionManager()
        {
            m_container = new SessionContainer[MaxCount];

            for (int i = 0; i < MaxCount; ++i)
                m_container[i] = new SessionContainer();
        }

        public override bool TryAdd(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("ClientSessionManager Add() session == null");
                return false;
            }
            return m_container[Get(session.GetNumber())].TryAdd(session);
        }

        public override void Remove(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("ClientSessionManager Remove() session == null");
                return;
            }

            m_container[Get(session.GetNumber())].Remove(session.GetNumber());
        }
    }
}


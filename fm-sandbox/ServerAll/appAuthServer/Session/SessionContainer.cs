using fmLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace appAuthServer
{
    /// <summary>
    /// 세션 container
    /// </summary>
    public class SessionContainer
    {
        //private readonly object m_lockObject = new object();
        private ConcurrentDictionary<long, SessionBase> m_dicSessions = new ConcurrentDictionary<long, SessionBase>();
        //private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        public bool TryAdd(SessionBase session)
        {
            return m_dicSessions.TryAdd(session.GetNumber(), session);
        }

        public void Remove(long managedid)
        {

            SessionBase session = null;
            m_dicSessions.TryRemove(managedid, out session);

            if (session != null)
                session = null;

            Logger.Debug("SessionContainer Remove");
        }

        public void RemoveAll()
        {
            m_dicSessions.Clear();
        }

        public bool TryFind(long managedid, out SessionBase session)
        {
            return m_dicSessions.TryGetValue(managedid, out session);
        }
    }
}

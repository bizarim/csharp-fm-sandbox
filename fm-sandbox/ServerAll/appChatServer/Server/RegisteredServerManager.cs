using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;
using System.Linq;

namespace appChatServer
{
    /// <summary>
    /// 접속 된 서버 관리자
    /// </summary>
    public class RegisteredServerManager : Singleton<RegisteredServerManager>
    {
        private readonly object m_lockObject = new object();

        protected Dictionary<eServerType, List<fmOtherServer>> m_dicOtherServers = new Dictionary<eServerType, List<fmOtherServer>>();

        public bool TryAdd(descOtherServer desc, ServerSession session)
        {
            if (null == desc)
            {
                Logger.Error("RegisteredServerManager Add desc == null");
                return false;
            }

            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(desc.m_eServerType))
                    m_dicOtherServers.Add(desc.m_eServerType, new List<fmOtherServer>());

                foreach (var node in m_dicOtherServers[desc.m_eServerType])
                {
                    if (node.m_desc.m_nSequence == desc.m_nSequence)
                    {
                        Logger.Error("RegisteredServerManager Same Sequence {0}", node.m_desc.m_nSequence);
                        return false;
                    }
                }

                m_dicOtherServers[desc.m_eServerType].Add(new fmOtherServer { m_session = session, m_desc = desc });

                Logger.Debug("RegisteredServerManager Add Sequence {0}", desc.m_nSequence);
            }

            return true;
        }

        public void Remove(descOtherServer desc)
        {
            if (null == desc)
            {
                Logger.Error("RegisteredServerManager Remove desc == null");
                return;
            }

            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(desc.m_eServerType))
                    return;

                fmOtherServer otherSvr = m_dicOtherServers[desc.m_eServerType].Find(x => x.m_desc.m_nSequence == desc.m_nSequence);
                if (null == otherSvr)
                    return;

                m_dicOtherServers[desc.m_eServerType].Remove(otherSvr);

                using (otherSvr)
                {
                    Logger.Debug("RegisteredServerManager Remove desc {0} {1}", otherSvr.m_desc.m_eServerType, otherSvr.m_desc.m_nSequence);
                }
            }
        }
        
        public List<fmOtherServer> Find(eServerType type)
        {
            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(type)) return null;
                return m_dicOtherServers[type];
            }
        }

        public fmOtherServer Find(eServerType type, int seq)
        {
            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(type))
                    return null;

                foreach (var node in m_dicOtherServers[type])
                {
                    if (null == node.m_desc)
                        continue;

                    if (node.m_desc.m_nSequence == seq)
                        return node;
                }

                return null;
            }
        }
    }
}

using fmCommon;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace appChatServer
{
    /// <summary>
    /// 클라이언트 세션 container
    /// </summary>
    public class ClientSessionContainer
    {
        private ConcurrentDictionary<long, ClientSession> m_dicSessions = new ConcurrentDictionary<long, ClientSession>();


        public int GetCount()
        {
            if (null == m_dicSessions) return 0;

            return m_dicSessions.Count;
        }

        public bool TryAdd(ClientSession session)
        {
            if (null == m_dicSessions) return false;
            return m_dicSessions.TryAdd(session.GetNumber(), session);
        }

        public bool TryRemove(long managedid, out ClientSession client)
        {
            client = null;
            if (null == m_dicSessions) return false;

            ClientSession session = null;


            return m_dicSessions.TryRemove(managedid, out session);
        }

        public void RemoveAll()
        {
            if (null == m_dicSessions) return;

            m_dicSessions.Clear();
        }

        public bool TryFind(long managedid, out ClientSession session)
        {
            session = null;
            if (null == m_dicSessions) return false;
            return m_dicSessions.TryGetValue(managedid, out session);

        }

        public bool TryFind(out List<ClientSession> list)
        {
            list = null;
            if (null == m_dicSessions) return false;

            return true;
        }

        public void Broadcasting(fmProtocol fp)
        {
            if (null == m_dicSessions) return;

            foreach (var node in m_dicSessions)
            {
                node.Value.SendPacket(fp);
            }
        }

        public void RelayPacket(Packet p)
        {
            if (null == m_dicSessions) return;

            foreach (var node in m_dicSessions)
            {
                node.Value.RelayPacket(p);
            }
        }
    }
}

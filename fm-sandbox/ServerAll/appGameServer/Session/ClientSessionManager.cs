using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Concurrent;

namespace appGameServer
{
    public class ClientSessionManager : Singleton<ClientSessionManager>
    {
        private readonly ConcurrentDictionary<long, ClientSessionContainer> m_container = null;

        private readonly int MaxCount = 10;
        protected int GetHash(long id) { return (int)(id% MaxCount); }

        public ClientSessionManager()
        {
            m_container = new ConcurrentDictionary<long, ClientSessionContainer>();

            for (int i = 0; i < MaxCount; ++i)
                m_container.TryAdd(i, new ClientSessionContainer());
        }

        public int GetCount()
        {
            int count = 0;

            foreach (var node in m_container)
            {
                count += node.Value.GetCount();
            }

            return count;
        }

        public void Add(ClientSession session)
        {
            if (null == session)
            {
                Logger.Error("ClientSessionManager Add() session == null");
                return;
            }

            long id = GetHash(session.GetNumber());
            ClientSessionContainer cscon = null;
            if (true == m_container.TryGetValue(id, out cscon))
            {
                cscon.TryAdd(session);

                //Logger.Debug("ClientSessionManager Add");
            }

            //Logger.Debug("ClientSessionManager end");
        }

        public void Remove(ClientSession session)
        {
            if (null == session)
            {
                Logger.Error("ClientSessionManager Remove() session == null");
                return;
            }

            long id = GetHash(session.GetNumber());
            ClientSessionContainer cscon = null;
            if (true == m_container.TryGetValue(id, out cscon))
            {
                ClientSession client = null;
                if (true == cscon.TryRemove(session.GetNumber(), out client))
                {
                    //Logger.Debug("2");
                    if (null == client)
                        return;
                    //Logger.Debug("3");

                    using (client)
                    {
                        //Logger.Debug("OutBeforeSession setLord null null");
                        client.SetLord(null, null);
                    }

                    client = null;
                    //Logger.Debug("ClientSessionManager end");
                }

                //Logger.Debug("ClientSessionManager Remove");
            }

            //Logger.Debug("ClientSessionManager end");
        }

        public void OutBeforeSession(long managedId, CloseReason reason = CloseReason.BeforeSession)
        {
            //Logger.Debug("OutBeforeSession start: {0}", managedId);
            long id = GetHash(managedId);
            ClientSessionContainer cscon = null;
            if (true == m_container.TryGetValue(id, out cscon))
            {
                //Logger.Debug("1: {0}", cscon.GetCount());
                ClientSession client = null;
                if (true == cscon.TryRemove(managedId, out client))
                {
                    //Logger.Debug("2");
                    if (null == client)
                        return;
                    //Logger.Debug("3");

                    using (client)
                    {
                        //Logger.Debug("OutBeforeSession setLord null null");
                        client.SetLord(null, null);
                        client.ForceDisconnect(reason);
                    }

                    client = null;
                }
            }

            //Logger.Debug("OutBeforeSession end");
        }

        public bool TryFind(long managedid, out ClientSession clinet)
        {
            clinet = null;
            long id = GetHash(managedid);
            ClientSessionContainer cscon = null;
            if (true == m_container.TryGetValue(id, out cscon))
            {
                return cscon.TryFind(managedid, out clinet);
            }

            return false;
        }

        //public bool TryFind(out ClientSessionContainer[] clinets)
        //{
        //    clinets = m_container;
        //    return true;
        //}

        public void Broadcasting(fmProtocol fp)
        {
            //foreach (var node in m_container)
            //{
            //    node.Value.Broadcasting(fp);
            //}
        }

        public void RelayPacet(Packet p)
        {
            foreach (var node in m_container)
            {
                node.Value.RelayPacket(p);
            }
        }

        //public void DisConSleeper()
        //{
        //    foreach (var node in m_container)
        //    {
        //        node.Value.DisConSleeper();
        //    }
        //}
    }
}

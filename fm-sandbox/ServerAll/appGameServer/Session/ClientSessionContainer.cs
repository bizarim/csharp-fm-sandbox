using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer
{
    public class ClientSessionContainer
    {
        //protected readonly object m_lockObject = new object();
        private ConcurrentDictionary<long, ClientSession> m_dicSessions = new ConcurrentDictionary<long, ClientSession>();
        //private Dictionary<long, ClientSession> m_dicSessions = new Dictionary<long, ClientSession>();
        //private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        private readonly ConcurrentDictionary<long, ClientSession> m_sleeper = new ConcurrentDictionary<long, ClientSession>();


        public int GetCount()
        {
            if (null == m_dicSessions) return 0;

            //lock (m_lockObject)
            //{
            //    return m_dicSessions.Count;
            //}

            return m_dicSessions.Count;
        }

        public bool TryAdd(ClientSession session)
        {
            if (null == m_dicSessions) return false;
            //lock (m_lockObject)
            //{
            //    if (true == m_dicSessions.ContainsKey(session.GetNumber()))
            //        return false;

            //    m_dicSessions.Add(session.GetNumber(), session);
            //}
            //return true;

            Logger.Debug("TryAdd {0}", session.GetNumber());

            return m_dicSessions.TryAdd(session.GetNumber(), session);
        }

        public bool TryRemove(long managedid, out ClientSession client)
        {
            client = null;
            if (null == m_dicSessions) return false;

            //Logger.Debug("TryRemove{0}", managedid);

            //lock (m_lockObject)
            //{
            //    if (false == m_dicSessions.ContainsKey(managedid))
            //    {
            //        //Logger.Error("Critical Error! SessionContainer");
            //        //throw new Exception("Remove Session fail not existed : " + managedid);
            //        return;
            //    }

            //    ClientSession session = null;
            //    m_dicSessions.TryGetValue(managedid, out session);

            //    m_dicSessions.Remove(managedid);

            //    if (session != null)
            //        session = null;

            //}

            //ClientSession session = null;
            

            return m_dicSessions.TryRemove(managedid, out client);
        }

        public void RemoveAll()
        {
            if (null == m_dicSessions) return;
            //lock (m_lockObject)
            //{
            //    m_dicSessions.Clear();
            //}

            m_dicSessions.Clear();
        }

        public bool TryFind(long managedid, out ClientSession session)
        {
            session = null;
            if (null == m_dicSessions) return false;
            return m_dicSessions.TryGetValue(managedid, out session);

            //lock (m_lockObject)
            //{
            //    if (false == m_dicSessions.ContainsKey(managedid))
            //        return false;

            //    return m_dicSessions.TryGetValue(managedid, out session);
            //}
        }

        public bool TryFind(out List<ClientSession> list)
        {
            //List<ClientSession> list = null;
            list = null;
            if (null == m_dicSessions) return false;

            //lock (m_lockObject)
            //{
            //    list = m_dicSessions.Values.ToList();
            //}

            return true;
        }

        //public void Broadcasting(fmProtocol fp)
        //{
        //    if (null == m_dicSessions) return;

        //    //List<ClientSession> list = null;

        //    //lock (m_lockObject)
        //    //{
        //    //    list = m_dicSessions.Values.ToList();
        //    //}

        //    //if (null == list) return;

        //    DateTime limitTime = fmServerTime.LimitBroadcast;
        //    foreach (var node in m_dicSessions.Values)
        //    {
        //        if (null == node) continue;

        //        if (node.GetLordState() == eLordState.Normal ||
        //            node.GetLordState() == eLordState.Maze)
        //        {
        //            if (true == node.IsAlive(limitTime))
        //            {
        //                node.SendPacket(fp);
        //                continue;
        //            }
        //        }

        //        //if (true == node.IsAlive(fmServerTime.LimitSleep))
        //        //{
        //        //    if (false == m_sleeper.ContainsKey(node.GetNumber()))
        //        //        m_sleeper.TryAdd(node.GetNumber(), node);
        //        //}
        //    }
        //}

        public void RelayPacket(Packet p)
        {
            DateTime limitTime = fmServerTime.LimitBroadcast;

            foreach (var node in m_dicSessions.Values)
            {
                if (null == node) continue;

                node.RelayPacket(p);

                //if (node.GetLordState() == eLordState.Normal ||
                //    node.GetLordState() == eLordState.Maze)
                //{
                //    if (true == node.IsAlive(limitTime))
                //    {
                //        node.RelayPacket(p);
                //        continue;
                //    }
                //}

                //if (true == node.IsAlive(fmServerTime.LimitSleep))
                //{
                //    if (false == m_sleeper.ContainsKey(node.GetNumber()))
                //        m_sleeper.TryAdd(node.GetNumber(), node);
                //}
            }
        }

        //public void DisConSleeper()
        //{
        //    foreach (var node in m_sleeper.Values)
        //    {
        //        if (null == node)
        //            continue;

        //        if (false == node.IsAlive(fmServerTime.LimitSleep))
        //            continue;

        //        node.ForceDisconnect(CloseReason.SessionSleep);
        //    }

        //    m_sleeper.Clear();
        //}
    }
}

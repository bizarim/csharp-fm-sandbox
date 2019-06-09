using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Linq;
using System.Collections.Generic;

namespace appCenterServer
{
    /// <summary>
    /// 서버 등록 관리자
    /// </summary>
    public class RegisteredServerManager : Singleton<RegisteredServerManager>
    {
        private readonly object m_lockObject = new object();

        protected Dictionary<eServerType, List<fmOtherServer>> m_dicOtherServers = new Dictionary<eServerType, List<fmOtherServer>>();

        /// <summary>
        /// 서버 리스트
        /// </summary>
        /// <returns></returns>
        public List<fmWorld> GetWorldList()
        {
            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(eServerType.Game))
                    return null;

                return m_dicOtherServers[eServerType.Game].ToFmWorld();
            }
        }

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

            if (desc.m_eServerType == eServerType.Game)
                SendWolrdList();

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

                fmOtherServer otherSvr = m_dicOtherServers[desc.m_eServerType].Find(x=> x.m_desc.m_nSequence == desc.m_nSequence);
                if (null == otherSvr)
                    return;

                m_dicOtherServers[desc.m_eServerType].Remove(otherSvr);

                using (otherSvr)
                {
                    Logger.Debug("RegisteredServerManager Remove desc {0} {1}", otherSvr.m_desc.m_eServerType, otherSvr.m_desc.m_nSequence);
                }
            }

            if (desc.m_eServerType == eServerType.Game)
                SendWolrdList();
        }

        public void UpdateWorldState(int sequence, int playercnt)
        {
            lock (m_lockObject)
            {
                if (true == m_dicOtherServers.ContainsKey(eServerType.Game))
                {
                    fmOtherServer otherSvr = m_dicOtherServers[eServerType.Game].Find(x=> x.m_desc.m_nSequence == sequence);
                    if (null != otherSvr)
                    {
                        //otherSvr.m_desc.m_eWorldState = worldState;
                        otherSvr.m_desc.m_nPlayerCount = playercnt;

                        Logger.Debug("UpdateWorldState desc {0} {1} {2}", otherSvr.m_desc.m_eServerType, otherSvr.m_desc.m_nSequence, playercnt);
                    }
                }

                Logger.Debug("RegisteredServerManager UpdateWorldState");
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

        public bool TryFindOpServer(out fmOtherServer op)
        {
            op = null;
            eServerType type = eServerType.Op;

            lock (m_lockObject)
            {

                if (false == m_dicOtherServers.ContainsKey(type))
                    return false;

                op = m_dicOtherServers[type].FirstOrDefault();
                if (op == null)
                    return false;
            }

            return true;
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

        public bool TryBroadcastToGameServer(Packet p)
        {
            eServerType type = eServerType.Game;

            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(type))
                    return false;


                foreach (var node in m_dicOtherServers[type])
                {
                    if (null == node.m_session)
                        continue;

                    node.m_session.RelayPacket(p);
                }
            }

            return true;
        }

        public bool TryBroadcastToAuthServer(Packet p)
        {
            eServerType type = eServerType.Auth;

            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(type))
                    return false;


                foreach (var node in m_dicOtherServers[type])
                {
                    if (null == node.m_session)
                        continue;

                    node.m_session.RelayPacket(p);
                }
            }

            return true;
        }

        public void SendWolrdList()
        {
            lock (m_lockObject)
            {
                if (false == m_dicOtherServers.ContainsKey(eServerType.Auth))
                    return;

                foreach (var node in m_dicOtherServers[eServerType.Auth])
                {
                    using (PT_CA_Server_GetWorldList_NT sendfmProtocol = new PT_CA_Server_GetWorldList_NT())
                    {
                        if (false == m_dicOtherServers.ContainsKey(eServerType.Game))
                        {
                            sendfmProtocol.m_eErrorCode = eErrorCode.Error;
                        }
                        else
                        {
                            sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                            sendfmProtocol.m_list = m_dicOtherServers[eServerType.Game].ToFmWorld();
                        }

                        node.m_session.SendPacket(sendfmProtocol);
                    }
                }
            }
        }

        public void Update()
        {
            lock (m_lockObject)
            {

            }
        }
    }
}

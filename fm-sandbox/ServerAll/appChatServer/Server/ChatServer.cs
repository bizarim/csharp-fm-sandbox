using appChatServer.Message;
using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appChatServer.Server
{
    /// <summary>
    /// 방송 서버 구성요소
    /// </summary>
    partial class ChatServer : appServer
    {
        protected MessageExecuter m_messageExecuter = new MessageExecuter();

        private ManagerSAEA m_managerSAEA = new ManagerSAEA();


        public SessionManager m_managerSessionForServer;

        // Netservice
        private NetService m_netServiceForClient;
        private NetService m_netServiceForAttach;

        // attachers
        protected CenterServerAttacher m_atchCenter = null;

        public ChatServer() { m_eServerType = eServerType.Chat; }

        public override bool Initialize()
        {
            System.Threading.Thread.Sleep(100);

            if (false == base.Initialize())
                return false;

            SyncMainRoute.Instance.Start();

            m_messageExecuter.Start(new MessageDispatcher(this));

            //if (false == DataBaseManager.Instance.Initialize(m_config.m_db))
            //    return false;

            List<Connector> listConnector = new List<Connector>();
            m_atchCenter = new CenterServerAttacher(this);
            listConnector.Add(m_atchCenter);

            m_managerSessionForServer = new ServerSessionManager();
            m_managerSAEA.Initialize(m_config);

            // managerSAEA
            m_managerSAEA.Initialize(m_config);

            // for client
            m_netServiceForClient = new NetService(this,
                new ClientSessionHandler(m_managerSAEA, OnAddClientSession, OnRemoveClientSession),
                new NetListenable(m_config.m_listnerClient));

            // for attacher
            m_managerSessionForServer = new ServerSessionManager();
            m_netServiceForAttach = new NetService(this,
                new ServerSessionHandler(m_managerSAEA, OnAddServerSession, OnRemoveServerSession, OnCheckingServer),
                new NetBothRole(m_config.m_listnerServer, listConnector));

            m_netServiceForClient.Initialize(OnHandleMessage);
            m_netServiceForAttach.Initialize(OnHandleMessage);


            m_atchCenter.OnAttach(m_config.m_nSequence, m_config.m_center, m_config.m_listnerClient);

            return true;
        }

        public override void Uninitialize()
        {
            base.Uninitialize();

            m_managerSAEA.Uninitialize();
        }

        public void OnAddClientSession(ClientSession session)
        {
            SyncMainRoute.Instance.Push(new Msg_Session_Add(this, session));
        }

        public void OnRemoveClientSession(ClientSession session)
        {
            SyncMainRoute.Instance.Push(new Msg_Session_Remove(this, session));
        }

        public void OnAddServerSession(SessionBase session)
        {
            m_managerSessionForServer.TryAdd(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Add(this, session));
        }

        public void OnRemoveServerSession(SessionBase session)
        {
            m_managerSessionForServer.Remove(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Remove(this, session));
        }

        public void SendPacketToCenter(fmProtocol fp)
        {
            //if (null == m_atchCenter)
            //{
            //    Logger.Error("m_atchCenter == null");
            //    return;
            //}

            //m_atchCenter.SendPacket(fp);
        }
    }
}

using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 센터 서버
    /// </summary>
    partial class CenterServer : appServer
    {
        protected MessageExecuter m_messageExecuter = new MessageExecuter();

        ManagerSAEA m_managerSAEA = new ManagerSAEA();

        public SessionManager m_managerSessionForServer;
        private NetService m_netServiceForServer;

        public override bool Initialize()
        {
            if (false == base.Initialize())
                return false;

            m_messageExecuter.Start(new MessageDispatcher(this));

            //if (false == DataBaseManager.Instance.Initialize(m_config.m_db))
            //    return false;

            m_managerSessionForServer = new ServerSessionManager();
            m_managerSAEA.Initialize(m_config);

            // Server
            m_netServiceForServer = new NetService(this,
                new ServerSessionHandler(m_managerSAEA, OnAddServerSession, OnRemoveServerSession, OnCheckingServer),
                new NetListenable(m_config.m_listnerServer));
            m_netServiceForServer.Initialize(OnHandleMessage);


            // [ MUSTBE BY KWJ ] : 20150205
            // 나중에 운영툴 들어가면 Center 서버에 붙어야 하니까.
            // 운영서버를 만들수도 있고 그때가서 판단하자.

            return Start();
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


        public override void Uninitialize()
        {
            base.Uninitialize();

        }

        public override bool Start()
        {
            if (false == base.Start())
                return false;

            m_netServiceForServer.Start();

            Logger.Info(string.Format("{0} -> Start", this.GetType().Name));

            return true;
        }

        public override bool Stop()
        {
            if (false == base.Stop())
                return false;

            return true;
        }
    }
}

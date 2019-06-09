using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appAuthServer
{
    /// <summary>
    /// 인증 서버 구성
    /// </summary>
    public partial class AuthServer : appServer
    {
        /// <summary>
        /// 메시지 처리기
        /// </summary>
        protected MessageExecuter m_messageExecuter = new MessageExecuter();

        /// <summary>
        /// SAEA manager
        /// </summary>
        private ManagerSAEA m_managerSAEA = new ManagerSAEA();

        /// <summary>
        /// 클라이언트 세션 관리자
        /// </summary>
        public SessionManager m_managerSessionForClient;
        /// <summary>
        /// 서버 세션 관리자
        /// </summary>
        public SessionManager m_managerSessionForServer;

        // Netservice
        private NetService m_netServiceForClient;
        private NetService m_netServiceForAttach;

        // attachers
        protected CenterServerAttacher m_atchCenter = null;

        public AuthServer() { m_eServerType = eServerType.Auth; }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            System.Threading.Thread.Sleep(3000);

            if (false == base.Initialize())
                return false;

            if (false == RedisMultiplexer.Instance.Start(eRedis.Token, rdToken()))
                return false;

            m_messageExecuter.Start(new MessageDispatcher(this), 3);

            List<Connector> listConnector = new List<Connector>();
            m_atchCenter = new CenterServerAttacher(this);
            listConnector.Add(m_atchCenter);

            // managerSAEA
            m_managerSAEA.Initialize(m_config);

            // for client
            m_managerSessionForClient = new ClientSessionManager();
            m_netServiceForClient = new NetService(this,
                new SessionHandler(m_managerSAEA, OnAddClientSession, OnRemoveClientSession),
                new NetListenable(m_config.m_listnerClient));

            // for attacher
            m_managerSessionForServer = new ServerSessionManager();
            m_netServiceForAttach = new NetService(this,
                new ServerSessionHandler(m_managerSAEA, OnAddServerSession, OnRemoveServerSession, OnCheckingServer),
                new NetAttachable(listConnector));

            m_netServiceForClient.Initialize(OnHandleMessage);
            m_netServiceForAttach.Initialize(OnHandleMessage);

            // attaching
            m_atchCenter.OnAttach(m_config.m_nSequence, m_config.m_center, m_config.m_listnerClient);

            return true;
        }

        /// <summary>
        /// 해제
        /// </summary>
        public override void Uninitialize()
        {
            base.Uninitialize();

            m_managerSAEA.Uninitialize();
        }

        /// <summary>
        /// 클라이언트 세션 등록 함수
        /// </summary>
        /// <param name="session"></param>
        public void OnAddClientSession(SessionBase session)
        {
            m_messageExecuter.Push(new Msg_Session_Add(m_managerSessionForClient, session));
            //m_managerSessionForClient.TryAdd(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Add(this, session));
        }

        /// <summary>
        /// 클라이언트 세션 해제 함수
        /// </summary>
        /// <param name="session"></param>
        public void OnRemoveClientSession(SessionBase session)
        {
            m_messageExecuter.Push(new Msg_Session_Remove(m_managerSessionForClient, session));
            //m_managerSessionForClient.Remove(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Remove(this, session));
        }

        /// <summary>
        /// 서버 세션 등록 함수
        /// </summary>
        /// <param name="session"></param>
        public void OnAddServerSession(SessionBase session)
        {
            m_managerSessionForServer.TryAdd(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Add(this, session));
        }

        /// <summary>
        /// 서버 세션 해제 함수
        /// </summary>
        /// <param name="session"></param>
        public void OnRemoveServerSession(SessionBase session)
        {
            m_managerSessionForServer.Remove(session);
            //SyncMainRoute.Instance.Push(new Msg_Session_Remove(this, session));
        }

        /// <summary>
        /// 서버 상태 체커
        /// </summary>
        /// <param name="session"></param>
        protected void OnCheckingServer(SessionBase session)
        {
            ServerSession ss = session as ServerSession;
            if (null == ss) return;
            if (null == ss.m_descServer) return;

            Logger.Debug("OnCheckingConnect-Disconnect {0}", ss.m_descServer.m_eServerType);

            if (ss.m_descServer.m_eServerType == eServerType.Center)
            {
                Logger.Warn("Disconnect to CenterServer");
            }
        }
    }
}

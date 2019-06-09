using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    public partial class GameServer : appServer
    {
        protected ThreadSeparator m_separator = new ThreadSeparator();
        protected MessageExecuter m_messageExecuter = new MessageExecuter();

        private ManagerSAEA m_managerSAEA = new ManagerSAEA();

        public SessionManager m_managerSessionForClient;
        public SessionManager m_managerSessionForServer;

        // Netservice
        private NetService m_netServiceForClient;
        private NetService m_netServiceForAttach;

        // attachers
        protected CenterServerAttacher m_atchCenter = null;

        //protected ChatServerAttacher m_chatSvr = null;

        public GameServer() { m_eServerType = eServerType.Game; }

        public override bool Initialize()
        {
            System.Threading.Thread.Sleep(1500);

            if (false == base.Initialize())
                return false;

            if (false == RedisMultiplexer.Instance.Start(eRedis.Token, rdToken()))
                return false;

            if (false == RedisMultiplexer.Instance.Start(eRedis.Game, rdGame()))
                return false;

            //if (false == RedisMultiplexer.Instance.Start(eRedis.Log, dbLog()))
            //    return false;

            m_messageExecuter.Start(new MessageDispatcher(this));
            m_separator.Start(this);

            

            List<Connector> listConnector = new List<Connector>();
            m_atchCenter = new CenterServerAttacher(this);
            ChatServerAttacher chatSvr = new ChatServerAttacher(this);
            //m_chatSvr = new ChatServerAttacher(this);
            listConnector.Add(m_atchCenter);
            listConnector.Add(chatSvr);

            ChatSessionManager.Instance.SetAttacher(chatSvr, m_config);

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
                new NetAttachable(listConnector));

            m_netServiceForClient.Initialize(OnHandleMessage);
            m_netServiceForAttach.Initialize(OnHandleMessage);

            // attaching
            m_atchCenter.OnAttach(m_config.m_nSequence, m_config.m_center, m_config.m_listnerClient);
            //m_atchCenter.OnAttach(m_config.m_nSequence, m_config.m_center, m_config.m_listnerClient);
            return true;
        }

        //public void ConnectAtCenter()
        //{
        //    m_atchCenter.OnAttach(m_config.m_nSequence, m_config.m_center, m_config.m_listnerClient);
        //    //m_chatSvr.OnAttach(m_config.m_nSequence, m_config.m_chat, m_config.m_listnerClient);
        //}


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
            //Logger.Debug("OnRemoveClientSession");
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
    }

    public class ChatSessionManager : Singleton<ChatSessionManager>
    {
        public eState State { get; set; }

        public ServerConfig m_config = null;
        public ChatServerAttacher m_chatSvr = null;

        public void SetAttacher(ChatServerAttacher ath, ServerConfig config)
        {
            m_chatSvr = ath;
            m_config = config;
            State = eState.eState_Stop;
        }

        public void Attach()
        {
            if (null == m_chatSvr)
                return;

            m_chatSvr.SetSession(null);
            m_chatSvr.OnAttach(m_config.m_nSequence, m_config.m_pirvateChat, m_config.m_listnerClient);
        }

        public void Run()
        {
            State = eState.eState_Run;
        }

        public bool IsConnect()
        {
            bool bCon = m_chatSvr.IsConnected();
            if (false == bCon)
            {
                if (State == eState.eState_Run)
                {
                    Logger.Warn("실행중 컨넥션이 끈겼을 때");
                    State = eState.eState_Ready;
                    Attach();
                }
            }

            return bCon;
        }

        public void SendPacketToChatServer(Packet p)
        {
            if (null == m_chatSvr)
            {
                Logger.Error("m_chatSvr == null");
                return;
            }

            if (IsConnect())
                m_chatSvr.SendPacket(p);
        }

        public void SendPacketToChatServer(fmProtocol p)
        {
            if (null == m_chatSvr)
            {
                Logger.Error("m_chatSvr == null");
                return;
            }

            if (IsConnect())
                m_chatSvr.SendPacket(p);
        }
    }

}

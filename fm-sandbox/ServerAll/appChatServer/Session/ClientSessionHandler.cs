using fmLibrary;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace appChatServer
{
    /// <summary>
    /// 클라이언트 message handler
    /// </summary>
    public class ClientSessionHandler : ISessionHandler
    {
        private IKeyGenerator m_keyGenerator;
        private ManagerSAEA m_managerSAEA;
        //private ClientSessionManager m_managerSession;

        public delegate void fnSessionHandler(ClientSession session);
        protected fnSessionHandler m_fnAddSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터
        protected fnSessionHandler m_fnRemoveSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터

        public ClientSessionHandler(ManagerSAEA saea, fnSessionHandler OnAdd, fnSessionHandler OnRemove)
        {
            m_keyGenerator = new LongKeyForClient();
            m_managerSAEA = saea;
            //m_managerSession = sessionManager;
            m_fnAddSession += OnAdd;
            m_fnRemoveSession += OnRemove;
        }

        public SessionBase CreateSession(Socket socket)
        {
            SocketAsyncEventArgs recvSAEA;
            if (!m_managerSAEA.TryPopRecvSAEA(out recvSAEA))
            {
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                return null;
            }

            SocketAsyncEventArgs sendSAEA;
            if (!m_managerSAEA.TryPopSendSAEA(out sendSAEA))
            {
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                return null;
            }

            ClientSession session = new ClientSession(socket, recvSAEA, sendSAEA, m_managerSAEA.m_pooledBufferManager);
            if (null == session)
            {
                recvSAEA.UserToken = null;
                sendSAEA.UserToken = null;
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                m_managerSAEA.PushSendSAEA(sendSAEA);
                Task.Run(() => { socket.CloseEx(); });

                return null;
            }

            long managedNumber = m_keyGenerator.Alloc();
            session.SetNumber(managedNumber, Environment.TickCount);
            //m_managerSession.Add(session);
            if (null != m_fnAddSession)
                m_fnAddSession(session);

            return session;
        }

        public void DestroySession(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("DestroySession session == null");
                return;
            }

            if (null != m_managerSAEA)
            {
                session.m_saeaReciver.UserToken = null;
                session.m_saeaSender.UserToken = null;
                // 버퍼 셋팅도 다시 해줘야 하나? 최초 BufferManager에서 할당받은 위치겠지만 
                m_managerSAEA.PushRecvSAEA(session.m_saeaReciver);
                m_managerSAEA.PushSendSAEA(session.m_saeaSender);
            }

            long id = session.GetNumber();
            m_keyGenerator.Free(id);

            ClientSession cs = session as ClientSession;
            //cs.SetPlayerCookie(null);

            //m_managerSession.Remove(cs);
            if (null != m_fnAddSession)
                m_fnRemoveSession(cs);

            //session = null;
            Logger.Debug("DestroySession {0}", id);
        }
    }
}

using fmLibrary;
using fmServerCommon;
using System.Net.Sockets;


namespace appGameServer
{
    public class ClientSession : Session
    {
        public ClientSession(Socket socket, SocketAsyncEventArgs recvSAEA, SocketAsyncEventArgs sendSAEA, PooledBufferManager pooledBufferManager)
            : base(socket, recvSAEA, sendSAEA, pooledBufferManager)
        {
        }


        protected fmLord m_lord;
        protected GameServer m_server;

        public long GetAccid() { return m_lord == null ? 0 : m_lord.AccId; }

        public void SetLord(fmLord lord, appServer server)
        {
            m_server = server as GameServer;
            m_lord = lord;
            if (null != m_lord)
            {
                m_lord.State = eLordState.Normal;
                m_lord.SessionId = GetNumber();
            }
        }

        public eLordState GetLordState()
        {
            if (null == m_lord)
                return eLordState.Logout;
            else
                return m_lord.State;
        }

        public bool TryGetLord(out fmLord lord)
        {
            if (null != m_lord)
                m_lord.ActTime = fmServerTime.Now;

            lord = m_lord;

            if (null == lord)
                return false;
            else
                return true;
        }

        protected override void OnClose()
        {
            //if (null != m_server)
            //{
            //    if (null != m_lord)
            //    {
            //        //m_server.Logout(m_lord);
            //        m_lord = null;
            //    }
            //    m_server = null;
            //}
        }
    }
}

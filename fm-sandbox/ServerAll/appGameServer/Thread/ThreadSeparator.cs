using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    public delegate void fnPushMessage(IMessage message, long accid = 0);

    public partial class ThreadSeparator
    {
        private readonly object m_objLock = new object();

        public enum eState { None, Run, };
        private eState m_eState;

        protected appServer m_server;
        protected Dictionary<eProtocolType, fnPushMessage> m_dicThread = new Dictionary<eProtocolType, fnPushMessage>();

        public bool Start(appServer server)
        {
            if (null == server) return false;
            if (eState.None != m_eState) return false;

            m_server = server;
            m_eState = eState.Run;
            InitSeparator();

            ArchiveExecuter.Instance.Start(m_server, 2);

            SyncMainRoute.Instance.Start();
            AsyncLordCreater.Instance.Start();

            BattleExecuter.Instance.Start();
            SyncMessageExecuter.Instance.Start(3);

            Logger.Info("Start ThreadSegregater");
            return true;
        }

        public void Stop()
        {
            m_eState = eState.None;
        }

        public bool TrySeparate(eProtocolType type, IMessage message, long accid = 0)
        {
            if (eState.None == m_eState)
                return false;

            if (false == m_dicThread.ContainsKey(type))
                return false;

            m_dicThread[type](message, accid);

            return true;
        }
    }
}

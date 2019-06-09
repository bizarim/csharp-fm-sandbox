using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;


namespace appAuthServer.Server
{
    public class SyncMainRoute : Singleton<SyncMainRoute>
    {
        private readonly object m_objLock = new object();

        private int m_nMaxCount = 0;
        private int m_nCurId = 0;
        private List<WorkerQueue> m_listWorker = new List<WorkerQueue>();

        public enum eState { None, Run, };
        private eState m_eState;

        private WorkerQueue GetWorker()
        {
            lock (m_objLock)
            {
                if (m_nMaxCount <= ++m_nCurId) m_nCurId = 0;
            }

            return m_listWorker[m_nCurId];
        }

        public bool Start(int cnt = 1)
        {
            if (eState.None != m_eState)
                return false;

            m_nMaxCount = cnt;
            m_listWorker.Clear();

            for (int i = 0; i < m_nMaxCount; ++i)
            {
                m_listWorker.Add(new WorkerQueue(i + 1));
            }

            m_eState = eState.Run;
            Logger.Info("Start SyncWaterRoute Count:{0}", m_nMaxCount);
            return true;
        }

        public void Stop()
        {
            m_eState = eState.None;
        }

        public bool Push(IMessage message)
        {
            if (null == message) return false;
            return GetWorker().Push(message);
        }
    }
}

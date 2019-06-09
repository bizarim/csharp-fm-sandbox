using System;

namespace appGameServer
{
    /// <summary>
    /// 영주 해제 처리
    /// </summary>
    public partial class fmLord : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                m_lordRandom = null;
                m_lordRanItem = null;

                AccId = 0;
                SessionId = 0;
                State = eLordState.None;

                if (m_base != null)
                {
                    m_base.Dispose();
                    m_base = null;
                }

                if (null != m_stat)
                {
                    m_stat.Dispose();
                    m_stat = null;
                }

                if (m_itemInven != null)
                {
                    m_itemInven.Dispose();
                    m_itemInven = null;
                }
                
            }
            m_disposed = true;
        }

        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmLord()
        {
            Dispose(false);
        }
    }
}

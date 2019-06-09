using appGameServer.Table;
using fmCommon;
using fmLibrary;
using System;

namespace appGameServer
{
    public class LordStat : IDisposable
    {
        private rdStat m_stat = null;

        public bool InitStat(rdStat stat)
        {
            m_stat = stat;
            return true;
        }

        public bool TryGetStat(out rdStat stat)
        {
            stat = m_stat;
            return true;
        }

        public bool TryResetStat(int lv)
        {
            if (lv <= 1)
                return true;

            //stats.TotalPoint = level * 3;
            m_stat.Point = theGameConst.StatPointPerLv * (lv - 1);
            m_stat.Hp = 1;
            m_stat.Def = 1;
            m_stat.Atk = 1;

            return true;
        }

        public void LevelUp(int incLv)
        {
            int addPoint = theGameConst.StatPointPerLv * incLv;

            m_stat.Point += addPoint;
            //m_stat.TotalPoint += addPoint;
        }

        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LordStat()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (null != m_stat)
                {
                    m_stat.Dispose();
                    m_stat = null;
                }
            }
            m_disposed = true;
        }
    }
}

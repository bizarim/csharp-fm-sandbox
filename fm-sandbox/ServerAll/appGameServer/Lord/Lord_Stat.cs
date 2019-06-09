using appGameServer.Table;
using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    /// <summary>
    /// 영주 상태
    /// </summary>
    public partial class fmLord
    {
        private LordStat m_stat = new LordStat();

        public bool InitStat(rdStat stat)
        {
            return m_stat.InitStat(stat);
        }

        public bool TryGetStat(out rdStat stat)
        {
            return m_stat.TryGetStat(out stat);
        }

        public bool TryResetStat()
        {
            return m_stat.TryResetStat(GetLv());
        }

        public bool TryLevelUp()
        {


            return true;
        }
    }
}

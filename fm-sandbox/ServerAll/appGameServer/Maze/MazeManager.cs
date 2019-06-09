using appGameServer.Table;
using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer
{
    public enum eMeetInMaze
    {
        Monster = 0,
        Pvp = 1,
        Hidden = 2,
    }

    public partial class LordManager : Singleton<LordManager>
    {
        private Random m_random = new Random();
        //private ConcurrentDictionary<long, fmLord> m_runners = new ConcurrentDictionary<long, fmLord>();

        private Dictionary<int, fmDataPvpDummy> m_dummys = null;

        public bool Load(fmDataTable table)
        {

            Dictionary<int, fmDataPvpDummy> dic = table.Find<fmDataPvpDummy>(eFmDataType.PvpDummy);
            if (null == dic)
                return false;

            m_dummys = dic;

            return true;
        }

        // 1. 매칭을 어떻게 해야 할까?
        private bool MatchingUser(fmLord lord, out fmLord target)
        {
            target = null;

            int gapLv = 5;
            int minLv = lord.GetLv() <= gapLv ? 1 : lord.GetLv() - gapLv;
            int maxLv = lord.GetLv() + gapLv;
            maxLv = 70 <= maxLv ? 70 : maxLv;

            DateTime limitTime = fmServerTime.Now.AddMinutes(-20);

            List<fmLord> rndlist = new List<fmLord>();
            int cnt = m_lords.Count;
            for (int i = cnt - 1; 0 <= i; --i)
            {
                fmLord node = m_lords.ElementAt(i).Value;
                if (lord == node) continue;
                if (node.GetLv() < minLv) continue;
                if (maxLv < node.GetLv()) continue;
                if (node.State == eLordState.Logout) continue;
                if (node.ActTime < limitTime) continue;

                rndlist.Add(node);

                if (30 < rndlist.Count)
                    break;
            }

            if (rndlist.Count <= 0)
                return false;

            target = rndlist[m_random.Next(0, rndlist.Count)];

            rndlist.Clear();
            rndlist = null;

            return true;
        }

    }

}

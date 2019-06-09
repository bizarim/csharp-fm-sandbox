using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace appGameServer
{
    /// <summary>
    /// 영주 관리자
    /// </summary>
    public partial class LordManager : Singleton<LordManager>
    {
        private readonly ConcurrentDictionary<long, fmLord> m_lords = new ConcurrentDictionary<long, fmLord>();

        public int GetCount() { return m_lords.Count; }

        public void Logout(fmLord lord)
        {
            if (null == lord)
            {
                Logger.Debug("LordManager Logout: lord == null");
                return;
            }

            fmLord outLord = null;
            if (true == m_lords.TryRemove(lord.AccId, out outLord))
            {
                Print();

                //if (outLord.State == eLordState.Maze)
                //{
                //    outLord.Logout();
                //}
                //else
                {
                    using (outLord)
                    {
                        outLord.Logout();
                    }
                }

                //Logger.Debug("LordManager TryRemove");
            }

            //Logger.Debug("LordManager Logout");

            lord = null;
        }

        public bool TryAdd(fmLord lord)
        {
            bool b = m_lords.TryAdd(lord.AccId, lord);
            Print();
            return b;
        }

        public bool CheckLogin(long accid, out fmLord lord)
        {
            return m_lords.TryGetValue(accid, out lord);
        }

        public bool TryFindWithTimeOut(out List<long> lords)
        {
            lords = new List<long>();
            lords.Clear();

            foreach (var node in m_lords)
            {

                if (node.Value.ActTime < fmServerTime.LimitSleep)
                {
                    lords.Add(node.Value.AccId);
                }
            }

            return true;
        }

        public bool TryFind(long accid, out fmLord lord)
        {
            return m_lords.TryGetValue(accid, out lord);
        }

        private void Print()
        {
            int playerCount = ClientSessionManager.Instance.GetCount();
            int lordCnt = m_lords.Count;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0} Connect Player: {1}, Lord: {2}", fmServerTime.Now, playerCount, lordCnt);
        }
    }
}

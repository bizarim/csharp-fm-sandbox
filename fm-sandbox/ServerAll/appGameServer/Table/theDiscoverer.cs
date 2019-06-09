using fmCommon;
using fmCommon.Formula;
using fmServerCommon;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace appGameServer.Table
{
    public class theDiscoverer
    {
        //static Random m_random = new Random();

        static fmGochaDrop m_droper = new fmGochaDrop();

        //static Dictionary<int, fmDataDHeart> m_dicDHeart = null;

        static Dictionary<int, eReward> m_matchGoblin = new Dictionary<int, eReward>();

        public static bool Load(fmDataTable table)
        {
            m_matchGoblin.Clear();
            m_matchGoblin.Add(20001, eReward.Gold);
            m_matchGoblin.Add(20002, eReward.Item);
            m_matchGoblin.Add(20003, eReward.Stone);
            m_matchGoblin.Add(20004, eReward.Ruby);

            return true;
        }

        private static eReward ModifyGochaByLv(Random lordRandom, int myLv, int otherLv, int itemDropRate, out int modify)
        {
            modify = 1;

            int mm = otherLv - myLv;
            if (mm < -5)
            {
                mm = -mm;

                int remain = m_droper.Max / mm;

                int hit = lordRandom.Next(0, m_droper.Min + remain);
                modify = mm / 2;

                //Console.WriteLine("remain: " + remain);
                //Console.WriteLine("max: " + (m_droper.Min + remain));
                //Console.WriteLine("hit: " + hit);
                //Console.WriteLine("modify: " + modify);
                return  m_droper.Gocha(hit);
            }
            else
            {
                int hit = lordRandom.Next(0, m_droper.Max + itemDropRate);
                return m_droper.Gocha(hit);
            }
        }

        private static void ModifyGochaByLvWithBoss(Random lordRandom, int myLv, int otherLv, int itemDropRate, out int modify)
        {
            modify = 1;

            int mm = otherLv - myLv;
            if (mm < -5)
            {
                mm = -mm;

                int remain = m_droper.Max / mm;

                modify = mm / 2;
            }
        }

        private static eReward ModifyGochaByLvWithGoblin(Random lordRandom, int goblinCode, int myLv, int otherLv, int itemDropRate, out int modify)
        {
            modify = 1;

            if (false == m_matchGoblin.ContainsKey(goblinCode))
                return eReward.None;

            eReward kind = m_matchGoblin[goblinCode];
            int mm = otherLv - myLv;
            if (mm < -12)
            {
                mm = -mm;

                int remain = 50 / mm;

                int hit = lordRandom.Next(0, 100);
                if (remain < hit)
                    return eReward.None;
            }

            return kind;
        }

        private static eReward ModifyGochaByLvInMaze(Random lordRandom, int myLv, int otherLv, int itemDropRate, out int modify)
        {
            modify = 1;

            int mm = otherLv - myLv;
            if (mm < -5)
            {
                mm = -mm;

                int remain = m_droper.Max / mm;

                int hit = lordRandom.Next(0, m_droper.Min + remain);
                modify = mm / 2;

                //Console.WriteLine("remain: " + remain);
                //Console.WriteLine("max: " + (m_droper.Min + remain));
                //Console.WriteLine("hit: " + hit);
                //Console.WriteLine("modify: " + modify);
                return m_droper.Gocha(hit);
            }
            else
            {
                int hit = lordRandom.Next(0, m_droper.Max + itemDropRate);
                return m_droper.Gocha(hit);
            }
        }
        
    }
}

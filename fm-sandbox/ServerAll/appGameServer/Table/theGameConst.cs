using fmCommon;
using fmLibrary;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class theGameConst
    {
        public static int MaxCntPerPage { get { return 20; } }
        public static int MaxInvenFullCnt { get { return 100; } }
        public static int MaxHaveItemCnt { get { return 60; } }
        public static int MaxRound { get { return 10; } }
        public static int MazeRoundCnt { get { return 5; } }
        //public static int MaxFloor { get { return 100; } }
        public static int MaxLv { get { return 70; } }

        public static int PvpWinPoint { get { return 10; } }
        public static int PvpLosePoint { get { return 3; } }

        public static int DropTicketCnt { get { return 1; } }
        public static int DropRubyCnt { get { return 3; } }
        public static int DropStoneCnt { get { return 1; } }
        public static int DropSealCnt { get { return 2; } }
        //public static int RemeltNeedStone { get { return 1; } }
        //public static int RerollNeedStone { get { return 1; } }
        //public static int RemeltNeedGold { get { return 200; } }

        public static int MaxDHeartCnt { get { return 3; } }
        public static int NeedTicket { get { return 5; } }
        public static int MaxDragonTombCnt { get { return 3; } }
        

        public static int MaxMissionRefreshCnt { get { return 3; } }
        public static int MaxMissionCnt { get { return 4; } }

        public static int MazeEnterLv { get { return 8; } }
        public static int DTombEnterLv { get { return 3; } }
        public static int DHeartEnterLv { get { return 50; } }
        public static int DHeartMaxRount { get { return 5; } }

        public static int StatPointPerLv { get { return 3; } }

        public static int RewardRubyNewRegion { get { return 2; } }
        public static int RewardRubyNewMap { get { return 5; } }
        public static int RewardRubyNextFloor { get { return 1; } }

        public static string Dummy { get { return "dummy"; } }

        //private static Dictionary<string, int> m_dicTypeInt = new Dictionary<string, int>();
        //private static Dictionary<string, long> m_dicTypeLong = new Dictionary<string, long>();

        private static string ChatSvrIp { get; set; }
        private static int ChatSvrPort { get; set; }

        public static bool Load(fmDataTable table, AttacherConfig chatConfig)
        {
            if (null == chatConfig)
                return false;

            if (true == string.IsNullOrEmpty(chatConfig.m_strIP))
                return false;

            if (0 == chatConfig.m_nPort)
                return false;

            ChatSvrIp = chatConfig.m_strIP;
            ChatSvrPort = chatConfig.m_nPort;

            //Dictionary<int, fmData> dics = table.Find(eFmDataType.GameConst);
            //if (null == dics)
            //    return false;

            //foreach (var node in dics)
            //{
            //    fmDataGameConst data = node.Value as fmDataGameConst;
            //    if (false == Add(data))
            //        return false;
            //}

            return true;
        }

        //private static bool Add(fmDataGameConst data)
        //{
        //    //string charKeyType = data.m_strKey.Substring(0, 1);

        //    //if (charKeyType.Equals("n")) m_dicTypeInt.Add(data.m_strKey, (int)data.m_biValue);
        //    //else if (charKeyType.Equals("b")) m_dicTypeLong.Add(data.m_strKey, data.m_biValue);
        //    //else return false;

        //    return true;
        //}

        public static fmGameConst ToClinet()
        {
            return new fmGameConst
            {
                MaxLv = MaxLv,
                MaxHaveItemCnt = MaxHaveItemCnt,
                MazeEnterLv = MazeEnterLv,
                DTombEnterLv = DTombEnterLv,
                ChatSvrIp = ChatSvrIp,
                ChatSvrPort = ChatSvrPort,
            };
        }
    }
}

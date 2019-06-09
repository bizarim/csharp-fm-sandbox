using fmCommon;
using System;
using System.Linq;
using System.Collections.Generic;
using fmLibrary;
using fmServerCommon;

namespace appGameServer.Table
{
    public class theMonsterPicker : Singleton<theMonsterPicker>
    {
        Random m_random = new Random();
        Dictionary<int, fmDataMonster> m_dicMonster = null;

        Dictionary<int, fmGochaExploreMonster> m_gochaExplore = new Dictionary<int, fmGochaExploreMonster>();
        Dictionary<eLevel, fmGochaTombMonster> m_gochaTomb = new Dictionary<eLevel, fmGochaTombMonster>();
        Dictionary<int, fmGochaMazeMonster> m_gochaMaze = new Dictionary<int, fmGochaMazeMonster>();

        // 인던 번호 -> 거점 번호
        Dictionary<int, Dictionary<int, fmGochaInDunMonster>> m_gochaInDun = new Dictionary<int, Dictionary<int, fmGochaInDunMonster>>();

        int MaxMazeFloor = 0;
        fmGochaMeet m_meet = null;
        //fmGochaGoblin m_mazeGoblin = null;

        public bool Load(fmDataTable table)
        {
            if (false == LoadGochaMeet()) return false;
            if (false == LoadExplore(table)) return false;
            if (false == LoadTomb(table)) return false;
            if (false == LoadMaze(table)) return false;
            if (false == LoadInDun(table)) return false;
            if (false == LoadMonster(table)) return false;
            return true;
        }

        private bool LoadGochaMeet()
        {
            m_meet = new fmGochaMeet(m_random);
            return true;
        }

        private bool LoadExplore(fmDataTable table)
        {
            Dictionary<int, fmDataExplore> dic = table.Find<fmDataExplore>(eFmDataType.Explore);
            if (null == dic)
                return false;

            m_gochaExplore.Clear();
            foreach (var node in dic)
            {
                fmGochaExploreMonster gm = new fmGochaExploreMonster(m_random);
                gm.Add(node.Value);
                m_gochaExplore.Add(node.Value.m_nLinkCode, gm);
            }

            return true;
        }

        private bool LoadTomb(fmDataTable table)
        {
            Dictionary<int, fmDataDragonTomb> dicTomb = table.Find<fmDataDragonTomb>(eFmDataType.DTomb);
            if (null == dicTomb)
                return false;

            m_gochaTomb.Clear();
            foreach (var node in dicTomb)
            {
                fmGochaTombMonster gm = new fmGochaTombMonster(m_random);
                gm.Add(node.Value);
                m_gochaTomb.Add(node.Value.m_eLevel, gm);
            }

            return true;
        }

        private bool LoadMaze(fmDataTable table)
        {
            Dictionary<int, fmData> dicMaze = table.Find(eFmDataType.Maze);
            if (null == dicMaze)
                return false;

            MaxMazeFloor = dicMaze.Count;

            m_gochaMaze.Clear();
            foreach (var node in dicMaze)
            {
                fmGochaMazeMonster gm = new fmGochaMazeMonster(m_random);
                gm.Add(node.Value as fmDataMaze);
                m_gochaMaze.Add(node.Value.Code, gm);
            }

            return true;
        }

        private bool LoadInDun(fmDataTable table)
        {
            Dictionary<int, fmDataInDun> dicInDun = table.Find<fmDataInDun>(eFmDataType.InDun);
            if (null == dicInDun)
                return false;

            m_gochaInDun.Clear();
            foreach (var node in dicInDun)
            {
                fmGochaInDunMonster gm = new fmGochaInDunMonster(m_random);
                gm.Add(node.Value);

                int indunCode = node.Value.m_nInDunCode;

                if (false == m_gochaInDun.ContainsKey(indunCode))
                    m_gochaInDun.Add(indunCode, new Dictionary<int, fmGochaInDunMonster>());

                m_gochaInDun[indunCode].Add(node.Value.m_nPlace, gm);
            }

            return true;
        }

        private bool LoadMonster(fmDataTable table)
        {
            m_dicMonster = table.Find<fmDataMonster>(eFmDataType.Monster);
            if (null == m_dicMonster)
                return false;

            return true;
        }

        //private bool LoadGoblin(fmDataTable table)
        //{

        //    Dictionary<int, fmDataGoblin> dicGoblin = table.Find<fmDataGoblin>(eFmDataType.Goblin);
        //    if (null == dicGoblin)
        //        return false;

        //    m_mazeGoblin = new fmGochaGoblin(m_random);
        //    m_exploreGoblin = new fmGochaGoblin(m_random);


        //    foreach (var node in dicGoblin)
        //    {
        //        if(node.Value.Code == 1)
        //            m_mazeGoblin.Add(node.Value);
        //        else if(node.Value.Code == 2)
        //            m_exploreGoblin.Add(node.Value);
        //    }

        //    return true;
        //}

        //private bool LoadDHeart(fmDataTable table)
        //{

        //    Dictionary<int, fmDataDHeart> dicDHeart = table.Find<fmDataDHeart>(eFmDataType.DHeart);
        //    if (null == dicDHeart)
        //        return false;

        //    m_dHeartMon.Clear();
        //    m_dHeartDragon.Clear();

        //    foreach (var node in dicDHeart)
        //    {
        //        fmGochaDHeartMonster dhm = new fmGochaDHeartMonster(m_random);
        //        dhm.Add(node.Value);
        //        m_dHeartMon.Add(node.Value.Code, dhm);

        //        fmGochaDHeartDragon dhd = new fmGochaDHeartDragon(m_random);
        //        dhd.Add(node.Value);
        //        m_dHeartDragon.Add(node.Value.Code, dhd);
        //    }

        //    return true;
        //}



        //public fmDataMonster GetMonInDHeart(int code)
        //{
        //    if (false == m_dHeartMon.ContainsKey(code))
        //        return null;

        //    int monCode = m_dHeartMon[code].Gocha();

        //    if (false == m_dicMonster.ContainsKey(monCode))
        //        return null;


        //    return m_dicMonster[monCode];
        //}

        //public fmDataMonster GetDragonInDHeart(int code)
        //{
        //    if (false == m_dHeartDragon.ContainsKey(code))
        //        return null;

        //    int monCode = m_dHeartDragon[code].Gocha();

        //    if (false == m_dicMonster.ContainsKey(monCode))
        //        return null;


        //    return m_dicMonster[monCode];
        //}

        public fmDataMonster GetMonInExplore(int code)
        {
            if (false == m_gochaExplore.ContainsKey(code))
                return null;

            int monCode = m_gochaExplore[code].Gocha();

            if (false == m_dicMonster.ContainsKey(monCode))
                return null;


            return m_dicMonster[monCode];
        }

        public fmDataMonster GetMonInMaze(int code)
        {
            if (false == m_gochaMaze.ContainsKey(code))
                return null;

            int monCode = m_gochaMaze[code].Gocha();

            if (false == m_dicMonster.ContainsKey(monCode))
                return null;

            return m_dicMonster[monCode];
        }

        //public int GetGoblinInMaze(out fmDataMonster data)
        //{
        //    data = null;
        //    int dropKind = 0;
        //    int monCode = 0;

        //    if (true == m_mazeGoblin.Gocha(out dropKind, out monCode))
        //    {
        //        if (true == m_dicMonster.ContainsKey(monCode))
        //        {
        //            data = m_dicMonster[monCode];
        //        }
        //    }

        //    return dropKind;
        //}

        //public int GetGoblinInExplore(Random lordRandom, int maxRound, out fmDataMonster data, out int intoRound)
        //{
        //    data = null;
        //    intoRound = 0;

        //    int dropKind = 0;
        //    int monCode = 0;
            
        //    if (null == lordRandom)
        //        return 0;

        //    intoRound = lordRandom.Next(1, maxRound);

        //    Logger.Debug("GetGoblinInExplore intoRound: {0}", intoRound);
        //    if (true == m_exploreGoblin.Gocha(out dropKind, out monCode))
        //    {
        //        //Console.WriteLine("monCode: "+ monCode);
        //        if (true == m_dicMonster.ContainsKey(monCode))
        //        {
        //            data = m_dicMonster[monCode];
        //            //if (null == data)
        //            //{
        //            //    Console.WriteLine("data: null");
        //            //}
        //        }
        //    }

        //    return dropKind;
        //}

        private fmDataMonster GetMonInDTomb(eLevel eLevel)
        {
            if (false == m_gochaTomb.ContainsKey(eLevel))
                return null;

            fmGochaTombMonster gm = m_gochaTomb[eLevel];

            int monCode = gm.Gocha();

            if (false == m_dicMonster.ContainsKey(monCode))
                return null;


            return m_dicMonster[monCode];
        }

        public bool TryGetMonInDun(int indunCode, int placeCode, out fmDataMonster data)
        {
            data = null;
            if (false == m_gochaInDun.ContainsKey(indunCode))
                return false;

            if (false == m_gochaInDun[indunCode].ContainsKey(placeCode))
                return false;

            int monCode = m_gochaInDun[indunCode][placeCode].Gocha();

            if (false == m_dicMonster.ContainsKey(monCode))
                return false;

            data = m_dicMonster[monCode];

            return true;
        }

        public bool TryGetDragon(eLevel eLevel, int lv, out fmDataMonster data)
        {
            data = null;
            //int hitLv = m_random.Next((lv + 1), (lv + 5));
            //hitElement = (eElement)m_random.Next((int)eElement.None, (int)eElement.Nature + 1);

            data = GetMonInDTomb(eLevel);
            if (null == data)
                return false;

            return true;
        }

        public bool MeetSomeone(Random lordRandom, out int intoRound)
        {
            intoRound = 0;
            //eMeetInMaze mim = m_meet.Gocha();

            if (null == lordRandom)
                return false;

            int hit = lordRandom.Next(0, 100);

            if (hit < 15)
            {
                intoRound = lordRandom.Next(2, 6);
                return true;
            }

            return false;

            //intoRound = 1;
            //return eMeetInMaze.Hidden;

            //intoRound = 1;
            //return eMeetInMaze.Pvp;
        }

        public bool MeetGoblinInMaze(Random lordRandom, int otherLv, int myLv)
        {
            if (null == lordRandom)
                return false;

            int rate = 280;

            int mm = otherLv - myLv;
            if (mm < -10)
            {
                mm = -mm;

                int hit = lordRandom.Next(0, 1000);

                int modify = rate / mm;

                if (hit < modify)
                    return true;

                return false;
            }
            else
            {
                int hit = lordRandom.Next(0, 1000);

                if (hit < rate)
                    return true;

                return false;
            }
        }

        public bool MeetGoblinInExplore(Random lordRandom, int maxRound, int otherLv, int myLv)
        {
            if (null == lordRandom)
                return false;

            int rate = (maxRound - 1) * 50;

            //Logger.Debug("MeetGoblinInExplore maxRound: {0}", maxRound);
            //Logger.Debug("MeetGoblinInExplore Rate: {0}", rate);

            int mm = otherLv - myLv;
            if (mm < 0)
            {
                mm = -mm;
            }
            else
            {
                mm = 0;
            }

            //Logger.Debug("MeetGoblin mm: {0}", mm);

            int hit = lordRandom.Next(0, 1000);

            int modify = rate - (mm * mm);
            if (modify < 0)
                modify = 0;

            //Logger.Debug("MeetGoblin hit: {0}", hit);
            //Logger.Debug("MeetGoblin modify: {0}", modify);

            if (hit < modify)
                return true;

            return false;
        }

        public bool CheckMaxMazeFloor(int floor)
        {
            if (MaxMazeFloor < floor)
                return false;

            return true;
        }
    }
}

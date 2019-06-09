
using fmCommon;
using System;

namespace appGameServer
{
    class LordExplore : LordDiscovery
    {
        protected int m_nMap = 0;

        public void SetExploreMap(int map)
        {
            m_nMap = map;
        }

        public int GetExploreMap()
        {
            return m_nMap;
        }
    }

    class LordMaze : LordDiscovery
    {
        protected int m_curFloor = 0;
        public void SetFloor(int floor) { m_curFloor = floor; }
        public int GetFloor() { return m_curFloor; }
        protected bool m_bPvp = false;
        public void SetPvp(bool b) { m_bPvp = b; }
        public bool GetPvp() { return m_bPvp; }
    }

    class LordInDun : LordDiscovery
    {
        protected int m_nInDunCode = 0;
        protected int m_nPlace = 0;
        protected int m_nBoss = 0;
        protected int m_nGoblin = 0;
        protected eOption m_nOpt = eOption.None;

        public void SetInDunPlace(int indunCode, int place, int boss, int goblin, eOption opt)
        {
            m_nInDunCode = indunCode;
            m_nPlace = place;
            m_nBoss = boss;
            m_nGoblin = goblin;
            m_nOpt = opt;
        }

        public void InitInDunPlace()
        {
            m_nInDunCode = 0;
            m_nPlace = 0;
            m_nBoss = 0;
            m_nGoblin = 0;
            m_nOpt = eOption.None;
        }

        public int GetInDunPlace()
        {
            return m_nPlace;
        }

        public int GetInDunCode()
        {
            return m_nInDunCode;
        }

        public int GetBoss()
        {
            return m_nBoss;
        }

        public int GetGoblin()
        {
            return m_nGoblin;
        }

        public eOption GetOpt()
        {
            return m_nOpt;
        }
    }
}

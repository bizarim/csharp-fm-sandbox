using appGameServer.Table;
using fmServerCommon;
using System;

namespace appGameServer
{
    public class LordBase : IDisposable
    {
        //private readonly object m_objLock = new object();

        private fmLordBase m_rdLordBase = null;

        public bool Block { get { return m_rdLordBase.Block; } }

        public bool InitLordInfo(fmLordBase info)
        {
            m_rdLordBase = info;
            //CheckDTomb();
            return true;
        }
        public bool TryGetLordBase(out fmLordBase lordInfo)
        {
            lordInfo = m_rdLordBase;
            return true;
        }
        public int GetFloorOneUnder() { return m_rdLordBase.Floor - 1; }
        public string GetName() { return m_rdLordBase.Name; }
        public int GetLv() { return m_rdLordBase.Lv; }
        public long GetExp() { return m_rdLordBase.Exp; }
        public long GetGold() { return m_rdLordBase.Gold; }
        public int GetRuby() { return m_rdLordBase.GameRuby + m_rdLordBase.AccRuby; }
        //public int GetDHeartFnc() { return m_rdLordBase.DHeartFnc; }
        public int GetStone() { return m_rdLordBase.Stone; }
        //public DateTime GetMissionTime() { return m_rdLordBase.MissionTime; }
        //public DateTime GetDTombTime() { return m_rdLordBase.DTombTime; }

        public bool SetPayment() { m_rdLordBase.Payment = true; return true; }


        public bool CheckSCKey(int scKey)
        {
            if (m_rdLordBase.SCKey <= 0)
                return false;

            return true;
        }
        public bool ConsumeSCKey(int scKey)
        {
            if (m_rdLordBase.SCKey < scKey)
                return false;

            m_rdLordBase.SCKey -= scKey;
            return true;
        }
        public bool AddSCKey(int scKey)
        {
            m_rdLordBase.SCKey += scKey;

            return true;
        }


        //public bool CheckDHeartCnt()
        //{
        //    if (m_rdLordBase.DHeartCnt <= 0)
        //        return false;

        //    return true;
        //}
        //public bool DecreaseDHeartCnt()
        //{
        //    if (false == CheckDHeartCnt())
        //        return false;

        //    m_rdLordBase.DHeartCnt -= 1;
        //    return true;
        //}
        //public bool ResetDHeartCnt()
        //{
        //    m_rdLordBase.DHeartCnt = theGameConst.MaxDHeartCnt;
        //    return true;
        //}
        //public bool CheckDHeartFnc(int cnt)
        //{
        //    if (m_rdLordBase.DHeartFnc < cnt)
        //        return false;

        //    return true;
        //}
        //public bool ConsumeDHeartFnc(int cnt)
        //{
        //    if (m_rdLordBase.DHeartFnc < cnt)
        //        return false;

        //    m_rdLordBase.DHeartFnc -= cnt;
        //    return true;
        //}
        //public bool AddDHeartFnc(int cnt)
        //{
        //    m_rdLordBase.DHeartFnc += cnt;

        //    return true;
        //}
        public bool CheckPvpPoint(int point)
        {
            if (m_rdLordBase.PvpPoint < point)
                return false;

            return true;
        }
        public bool ConsumePvpPoint(int point)
        {
            if (m_rdLordBase.PvpPoint < point)
                return false;

            m_rdLordBase.PvpPoint -= point;
            return true;
        }
        public bool AddPvpPoint(int point)
        {
            m_rdLordBase.PvpPoint += point;

            return true;
        }
        public bool ConsumeStone(int stone)
        {
            if (m_rdLordBase.Stone < stone)
                return false;

            m_rdLordBase.Stone -= stone;
            return true;
        }
        public bool AddStone(int stone)
        {
            m_rdLordBase.Stone += stone;

            return true;
        }
        public bool CheckStone(int stone)
        {
            if (m_rdLordBase.Stone < stone)
                return false;

            return true;
        }
        public bool CheckGold(int gold)
        {
            if (m_rdLordBase.Gold < gold)
                return false;

            return true;
        }
        public bool ConsumeGold(long gold)
        {
            if (m_rdLordBase.Gold < gold)
                return false;

            m_rdLordBase.Gold -= gold;
            return true;
        }
        public bool AddGold(long gold)
        {
            m_rdLordBase.Gold += gold;
            return true;
        }
        public bool CheckRuby(int ruby)
        {
            if ((m_rdLordBase.GameRuby + m_rdLordBase.AccRuby) < ruby)
                return false;

            return true;
        }
        public bool ConsumeRuby(int needRuby)
        {
            if ((m_rdLordBase.GameRuby + m_rdLordBase.AccRuby) < needRuby)
                return false;

            if (m_rdLordBase.GameRuby < needRuby)
            {
                m_rdLordBase.AccRuby = m_rdLordBase.AccRuby - (needRuby - m_rdLordBase.GameRuby);
                m_rdLordBase.GameRuby = 0;
            }
            else
            {
                m_rdLordBase.GameRuby -= needRuby;
            }

            return true;
        }
        public bool AddGameRuby(int ruby)
        {
            m_rdLordBase.GameRuby += ruby;
            return true;
        }
        public bool AddAccRuby(int ruby)
        {
            m_rdLordBase.AccRuby += ruby;
            return true;
        }
        public bool CheckFloor(int floor)
        {
            if (floor <= m_rdLordBase.Floor)
                return true;

            //if (m_rdLordBase.Floor == floor)
            //    return true;

            return false;
        }
        public bool Upstairs(int floor)
        {
            int temp = floor + 2;

            if (m_rdLordBase.Floor < temp)
            {
                m_rdLordBase.Floor = temp;
                return true;
            }

            return false;
        }
        public void AddExp(long exp)
        {
            m_rdLordBase.Exp += exp;
        }
        public void LevelUp(int lv, long exp)
        {
            m_rdLordBase.Lv = lv;
            m_rdLordBase.Exp = exp;
        }
        //public void SetMiisionTime(DateTime time)
        //{
        //    m_rdLordBase.MissionTime = time;
        //}
        //public void SetDTombTime(DateTime time)
        //{
        //    m_rdLordBase.DTombTime = time;
        //}
        public int GetDTombCnt() { return m_rdLordBase.DTombCnt; }
        public bool CheckDTombCnt()
        {
            if (m_rdLordBase.DTombCnt <= 0)
                return false;

            return true;
        }
        public bool DecreaseDTombCnt()
        {
            if (false == CheckDTombCnt())
                return false;

            m_rdLordBase.DTombCnt -= 1;
            return true;
        }
        public bool ResetDTombCnt()
        {
            m_rdLordBase.DTombCnt = theGameConst.MaxDragonTombCnt;
            return true;
        }
        //private void CheckDTomb()
        //{
        //    if (m_rdLordBase.DTombTime <= fmServerTime.Now)
        //    {
        //        SetDTombTime(fmServerTime.Now.Date.AddDays(1));
        //        ResetDTombCnt();
        //    }
        //}

        //Seal
        //public bool CheckSeal(int seal)
        //{
        //    if (m_rdLordBase.Seal < seal)
        //        return false;

        //    return true;
        //}
        //public bool ConsumeSeal(int seal)
        //{
        //    if (m_rdLordBase.Seal < seal)
        //        return false;

        //    m_rdLordBase.Seal -= seal;
        //    return true;
        //}
        //public bool AddSeal(int seal)
        //{
        //    m_rdLordBase.Seal += seal;
        //    return true;
        //}


        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LordBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {

            }
            m_disposed = true;
        }
    }
}

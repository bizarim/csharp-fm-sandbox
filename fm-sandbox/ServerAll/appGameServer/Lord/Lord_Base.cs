using appGameServer.Table;
using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace appGameServer
{
    public enum eLordState
    {
        None = 0,
        Create,
        Delete,
        Normal,
        //Maze,
        Logout,
        Login,
    }

    /// <summary>
    /// 영주 베이스
    /// </summary>
    public partial class fmLord
    {
        protected Random m_lordRandom = new Random();
        protected Random m_lordRanItem = new Random();

        public eLordState State { get; set; }
        public long AccId { get; set; }
        public long SessionId { get; set; }
        public DateTime ActTime = new DateTime();

        private LordBase m_base = new LordBase();

        public bool InitLordBase(fmLordBase info) { return m_base.InitLordInfo(info); }

        public bool TryGetLordBase(out fmLordBase lordInfo) { return m_base.TryGetLordBase(out lordInfo); }

        public bool TryLordBaseToClinet(out rdLordInfo lordInfo)
        {
            fmLordBase lb = null;
            m_base.TryGetLordBase(out lb);
            lordInfo = lb.ToClient();
            return true;
        }

        public bool Block { get { return m_base.Block; } }

        public void GetInformation(PT_LordInfo sendfmProtocol)
        {
            TryLordBaseToClinet(out sendfmProtocol.m_rdLordInfo);
            TryGetStat(out sendfmProtocol.m_rdStat);
        }

        public bool CheckSCKey(int scKey) { return m_base.CheckSCKey(scKey); }
        public bool ConsumeSCKey(int scKey) { return m_base.ConsumeSCKey(scKey); }
        public bool AddSCKey(int scKey) { return m_base.AddSCKey(scKey); }
        public bool CheckPvpPoint(int point) { return m_base.CheckPvpPoint(point); }
        public bool ConsumePvpPoint(int point) { return m_base.ConsumePvpPoint(point); }
        public bool AddPvpPoint(int point) { return m_base.AddPvpPoint(point); }
        public bool ConsumeStone(int stone) { return m_base.ConsumeStone(stone); }
        public bool AddStone(int stone) { return m_base.AddStone(stone); }
        public bool CheckStone(int stone) { return m_base.CheckStone(stone); }
        public bool CheckGold(int gold) { return m_base.CheckGold(gold); }
        public bool ConsumeGold(long gold) { return m_base.ConsumeGold(gold); }
        public bool AddGold(long gold) { return m_base.AddGold(gold); }

        public bool CheckRuby(int ruby) { return m_base.CheckRuby(ruby); }
        public bool ConsumeRuby(int ruby) { return m_base.ConsumeRuby(ruby); }
        public bool AddGameRuby(int ruby) { return m_base.AddGameRuby(ruby); }
        public bool AddAccRuby(int ruby) { return m_base.AddAccRuby(ruby); }
        public bool SetPayment() { return m_base.SetPayment(); }

        public int GetLv() { return m_base.GetLv(); }
        public long GetGold() { return m_base.GetGold(); }
        public int GetRuby() { return m_base.GetRuby(); }
        public int GetStone() { return m_base.GetStone(); }

        public bool CheckFloor(int floor) { return m_base.CheckFloor(floor); }
        public bool Upstairs(int floor) { return m_base.Upstairs(floor); }
        public int GetFloorOneUnder() { return m_base.GetFloorOneUnder(); }
        public string GetName() { return m_base.GetName(); }

        public bool LevelUpAddExp(long exp)
        {
            if (theGameConst.MaxLv <= m_base.GetLv())
                return false;

            m_base.AddExp(exp);
            return LevelUp();
        }

        private bool LevelUp()
        {
            int lv = m_base.GetLv();
            long exp = m_base.GetExp();

            if (theGameConst.MaxLv <= lv)
                return false;

            int incLv = 0;
            long tempExp = exp;
            bool bLvup = false;

            for (int i = lv; i <= theGameConst.MaxLv; ++i)
            {
                fmDataExp data = theFmDataFinder.Find<fmDataExp>(i);
                if (null == data)
                    return false;

                long remainExp = data.m_biNeedExp - tempExp;

                if (remainExp <= 0)
                {
                    incLv += 1;
                    tempExp -= data.m_biNeedExp;
                    bLvup = true;
                }
                else
                {
                    if (false == bLvup)
                        tempExp = remainExp;
                    break;
                }
            }

            if (incLv <= 0)
                return false;

            int sumLv = (lv + incLv);


            if (theGameConst.MaxLv <= sumLv)
            {
                sumLv = 70;
                tempExp = 0;
            }

            m_base.LevelUp(sumLv, tempExp);
            m_stat.LevelUp(incLv);

            return true;
        }

        private int GetDTombCnt() { return m_base.GetDTombCnt(); }
        private bool CheckDTombCnt() { return m_base.CheckDTombCnt(); }
        private bool DecreaseDTombCnt() { return m_base.DecreaseDTombCnt(); }
        private bool ResetDTombCnt() { return m_base.ResetDTombCnt(); }

        private void GetDiscovery(List<fmDiscovery> discoverys, out List<rdItem> items, out fmDiscoveryRs fdrs, int rewardRuby = 0, eOption selectedOpt = eOption.None)
        {
            fdrs = new fmDiscoveryRs();

            items = new List<rdItem>();
            items.Clear();

            if (0 < rewardRuby)
            {
                AddGameRuby(rewardRuby);
                fdrs.Ruby += rewardRuby;
            }

            foreach (var nodes in discoverys)
            {
                foreach (var node in nodes.DropList)
                {
                    if (node.Kind == eReward.Gold)
                    {
                        int gold = int.Parse(node.Contents);
                        AddGold(gold);
                        fdrs.Gold += gold;
                    }
                    else if (node.Kind == eReward.Stone)
                    {
                        int stone = int.Parse(node.Contents);
                        AddStone(stone);
                        fdrs.Stone += stone;
                    }
                    else if (node.Kind == eReward.SCKey)
                    {
                        int scKey = int.Parse(node.Contents);
                        AddSCKey(scKey);
                        fdrs.SCKey += scKey;
                    }
                    else if (node.Kind == eReward.Ruby)
                    {
                        int ruby = int.Parse(node.Contents);
                        AddGameRuby(ruby);
                        fdrs.Ruby += ruby;
                    }
                    //else if (node.Kind == eReward.DHeart)
                    //{
                    //    int dHeart = int.Parse(node.Contents);
                    //    AddDHeartFnc(dHeart);
                    //    fdrs.DHeart += dHeart;
                    //}
                    //else if (node.Kind == eReward.Seal)
                    //{
                    //    int seal = int.Parse(node.Contents);
                    //    AddSeal(seal);
                    //    fdrs.Seal += seal;
                    //}
                    else if (node.Kind == eReward.Item)
                    {
                        rdItem item = null;
                        if (eOption.None == selectedOpt)
                            theOptionPicker.Instance.GetItem(new JavaScriptSerializer().Deserialize<fmDropItem>(node.Contents), out item);
                        else
                            theOptionPicker.Instance.GetItemWithBoss(new JavaScriptSerializer().Deserialize<fmDropItem>(node.Contents), selectedOpt, out item);

                        if (null == item)
                            continue;

                        if (item.Parts == eParts.Jewel)
                        {
                            //Console.WriteLine(item.Parts);
                            if (eGrade.Epic < item.Grade)
                            {
                                //Console.WriteLine(item.Grade);
                                // 방송
                                //ArchiveExecuter.Instance.Push(new Msg_Broadcast_Public_NT(GetName(), item));
                            }
                        }

                        if ((GetLv() + 10) < item.Lv)
                            Logger.Error("Cheater Name:{0} , AccId:{1}, Lv:{2}, ItemLv:{3}", GetName(), AccId, GetLv(), item.Lv);

                        TryTakeItem(item);
                        items.Add(item);
                    }
                }

                fdrs.Exp += nodes.Exp;
            }

            fdrs.IsLevelUp = LevelUpAddExp(fdrs.Exp);
        }
    }
}

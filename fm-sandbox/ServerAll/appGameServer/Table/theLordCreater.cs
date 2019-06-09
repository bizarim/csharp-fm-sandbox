using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public static class theLordCreater
    {
        //static Dictionary<int, fmDataMap> m_maps = null;

        static List<rdMap> m_maps = new List<rdMap>();
        static List<rdInDun> m_inDuns = new List<rdInDun>();

        public static bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataExplore> dic = theFmDataFinder.Find<fmDataExplore>(eFmDataType.Explore);
            if (null == dic)
                return false;

            m_maps.Clear();

            foreach (var node in dic)
            {
                m_maps.Add(new rdMap { Code = node.Value.m_nLinkCode, Open = node.Value.m_nLinkCode == 101 ? true : false });
            }

            m_inDuns.Clear();
            foreach (var node in dic)
            {
                if (1 == node.Value.m_nInDun)
                    m_inDuns.Add(new rdInDun { Code = node.Value.m_nLinkCode, Shortcut = 0, CurPlace = 0, Forge = 0 });
            }

            return true;
        }

        public static bool TryCreate(long accid, string name, out fmLord o_lord)
        {
            o_lord = new fmLord();
            o_lord.ActTime = fmServerTime.Now;
            o_lord.State = eLordState.Create;
            o_lord.AccId = accid;

            fmLordBase lordInfo = new fmLordBase
            {
                Name = name,
                Lv = 1,
                Exp = 0,
                Gold = 4000,
                GameRuby = 20,
                AccRuby = 0,
                Stone = 20,
                //Ticket = 10,
                PvpPoint = 5,
                Floor = 2,
                DTombCnt = 9,
                //DTombTime = fmServerTime.Epoch,
                //MissionTime = fmServerTime.Epoch
                //DHeartCnt = 5,
                //DHeartFnc = 0,
                Payment = false,
                SCKey = 3,
                //Seal = 10,
            };

            rdStat lordStat = new rdStat
            {
                //TotalPoint = 3,
                Point = 0,
                Atk = 1,
                Def = 1,
                Hp = 1
            };

            fmMissionBase missionBase = new fmMissionBase
            {
                RefreshCnt = theGameConst.MaxMissionRefreshCnt,
                MissionTime = fmServerTime.Epoch,
            };


            List<rdMap> maps = new List<rdMap>();
            foreach (var node in m_maps)
            {
                maps.Add(new rdMap { Code = node.Code, Open =node.Open });
            }
            List<rdInDun> inDuns = new List<rdInDun>();
            foreach (var node in m_inDuns)
            {
                inDuns.Add(new rdInDun { Code = node.Code, Shortcut = 0, CurPlace = 0, Forge = 0 });
            }

            List<rdItem> items = new List<rdItem>();
            {
                // 무기
                rdItem item = new rdItem { Slot = 1, Lv = 1, Grade = eGrade.Normal, Parts = eParts.Weapon, Code = 1, Equip = true, BaseOpt = new List<rdOption>(), AddOpts = new List<rdOption>() };
                item.BaseOpt.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.BWDMin, Value = 87 });
                item.BaseOpt.Add(new rdOption { Index = 2, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.BWDMax, Value = 97 });
                item.BaseOpt.Add(new rdOption { Index = 3, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.AS, Value = 1.02f });
                item.BaseOpt.Add(new rdOption { Index = 4, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.Element, Value = 0 });

                item.AddOpts.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.HP, Value = 15 });
                items.Add(item);
            }
            {
                // 가슴
                rdItem item = new rdItem { Slot = 2, Lv = 1, Grade = eGrade.Normal, Parts = eParts.Armor, Code = 20, Equip = true, BaseOpt = new List<rdOption>(), AddOpts = new List<rdOption>() };
                item.BaseOpt.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.DEF, Value = 6 });
                item.AddOpts.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.ResistAll, Value = 11 });
                items.Add(item);
            }
            {
                // 반지
                rdItem item = new rdItem { Slot = 3, Lv = 1, Grade = eGrade.Normal, Parts = eParts.Ring, Code = 9, Equip = true, BaseOpt = new List<rdOption>(), AddOpts = new List<rdOption>() };
                item.BaseOpt.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.ResistNone, Value = 9 });
                item.AddOpts.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.BWDMin, Value = 1 });
                items.Add(item);
            }
            {
                // 바지
                rdItem item = new rdItem { Slot = 4, Lv = 1, Grade = eGrade.Normal, Parts = eParts.Pants, Code = 16, Equip = true, BaseOpt = new List<rdOption>(), AddOpts = new List<rdOption>() };
                item.BaseOpt.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.HP, Value = 16 });
                item.AddOpts.Add(new rdOption { Index = 1, Grade = eOptGrade.Normal, Remelt = false, Kind = eOption.BWDMax, Value = 2 });
                items.Add(item);
            }

            o_lord.State = eLordState.Create;
            o_lord.InitLordBase(lordInfo);
            o_lord.InitStat(lordStat);
            o_lord.InitItems(items);

            return true;
        }
    }
}

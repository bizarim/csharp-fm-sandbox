using fmCommon;
using fmLibrary;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public class fmOptListDrop
    {
        //List<eOption> list_Normal = new List<eOption>();
        //List<eOption> list_Legend = new List<eOption>();

        //List<eOption> list_SetUnder25 = new List<eOption>();
        //List<eOption> list_SetUnder70 = new List<eOption>();

        //List<eOption> list_Jewel = new List<eOption>();
        //List<eOption> list_JewelLegend = new List<eOption>();

        List<int> m_nLvRange = new List<int>();

        // 레벨
        Dictionary<eGrade, Dictionary<int, Dictionary<eParts, List<eOption>>>> m_optList = new Dictionary<eGrade, Dictionary<int, Dictionary<eParts, List<eOption>>>>();


        public bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataOption> dic = table.Find<fmDataOption>(eFmDataType.Option);
            if (null == dic) return false;

            m_optList.Add(eGrade.Normal,    new Dictionary<int, Dictionary<eParts, List<eOption>>>());
            m_optList.Add(eGrade.Legend,    new Dictionary<int, Dictionary<eParts, List<eOption>>>());
            m_optList.Add(eGrade.Set,       new Dictionary<int, Dictionary<eParts, List<eOption>>>());

            foreach (var node in dic)
            {
                int lv = node.Value.m_nAppearLv;
                if (false == m_nLvRange.Contains(lv))
                    m_nLvRange.Add(lv);

                foreach (var item in m_optList)
                {
                    if (false == item.Value.ContainsKey(lv))
                    {
                        item.Value.Add(lv, new Dictionary<eParts, List<eOption>>());
                        item.Value[lv].Add(eParts.Weapon,   new List<eOption>());
                        item.Value[lv].Add(eParts.Necklace, new List<eOption>());
                        item.Value[lv].Add(eParts.Ring,     new List<eOption>());
                        item.Value[lv].Add(eParts.Belt,     new List<eOption>());
                        item.Value[lv].Add(eParts.Gloves,   new List<eOption>());
                        item.Value[lv].Add(eParts.Pants,    new List<eOption>());
                        item.Value[lv].Add(eParts.Armor,    new List<eOption>());
                        item.Value[lv].Add(eParts.Head,     new List<eOption>());
                        item.Value[lv].Add(eParts.Jewel,    new List<eOption>());
                    }
                }
            }

            foreach (var node in dic)
            {
                int lv = node.Value.m_nAppearLv;
                eOption opt = node.Value.m_eOption;
                eOptGrade optGrade = (eOptGrade)((int)opt >> 8);

                foreach (var beyond in node.Value.m_nArrBeyond)
                {
                    if ((eBeyond)beyond == eBeyond.None)
                    {
                        int cnt = node.Value.m_nArrParts.Length;
                        for (int i = 0; i < cnt; ++i)
                        {
                            eParts parts = (eParts)node.Value.m_nArrParts[i];

                            if (optGrade < eOptGrade.Legend)
                            {
                                foreach (var list in m_optList[eGrade.Normal])
                                {
                                    if (lv <= list.Key)
                                        list.Value[parts].Add(opt);
                                }
                            }
                            else if(eOptGrade.Legend == optGrade)
                            {
                                foreach (var list in m_optList[eGrade.Legend])
                                {
                                    if (lv <= list.Key)
                                        list.Value[parts].Add(opt);
                                }
                            }
                            else if (eOptGrade.Set == optGrade)
                            {
                                foreach (var list in m_optList[eGrade.Set])
                                {
                                    if (lv <= list.Key)
                                        list.Value[parts].Add(opt);
                                }
                            }
                        }
                    }
                }
            }

            m_nLvRange = m_nLvRange.OrderBy(x=>x).ToList();

            return true;
        }

        private int GetRangeLv(int lv)
        {
            int cnt = m_nLvRange.Count;

            int preLv = 1;

            for (int i = 0; i < cnt; ++i)
            //foreach(var node in m_nLvRange)
            {
                int rangeLv = m_nLvRange[i];

                if (lv < rangeLv)
                    return preLv;

                preLv = rangeLv;
            }

            return preLv;
        }

        public List<eOption> GetList(eGrade grade, eParts parts, int lv)
        {
            Dictionary<int, Dictionary<eParts, List<eOption>>> dic = null;

            if (eGrade.Legend == grade)
                dic = m_optList[grade];
            else if (eGrade.Set == grade)
                dic = m_optList[grade];
            else
                dic = m_optList[eGrade.Normal];

            int rangeLv = GetRangeLv(lv);

            if (true == dic.ContainsKey(rangeLv))
            {
                //Logger.Debug("dic.ContainsKey(rangeLv) {0},{1},{2}", grade, parts, lv);
                if (true == dic[rangeLv].ContainsKey(parts))
                {
                    Logger.Debug("grade{0}/dic[{1}].CKey({2})/lv{3}", grade, rangeLv, parts, lv);
                    return dic[rangeLv][parts].ToList();
                }
                else
                {
                    Logger.Error("2-GetList {0},{1},{2},{3}", grade, parts, lv, rangeLv);
                }
            }
            else
            {
                Logger.Error("1-GetList {0},{1},{2},{3}", grade, parts, lv, rangeLv);
            }

            return dic[1][parts].ToList();

            //if (false == m_optList.ContainsKey(rangeLv))
            //    return null;

            //if (false == m_optList[rangeLv].ContainsKey(parts))
            //    return null;

            //return m_optList[rangeLv][parts].ToList();
        }

        //private bool CheckLvRange(int lv)
        //{
        //    foreach (var node in m_nLvRange)
        //    {
        //        node
        //    }

        //    return false;
        //}

        //public List<eOption> GetList(eGrade grade, eParts parts, int lv = 25)
        //{
        //    if (parts == eParts.Jewel)
        //    {
        //        switch (grade)
        //        {
        //            case eGrade.Legend: return list_JewelLegend.ToList();
        //            //case eGrade.Set:
        //            default: return list_Jewel.ToList();
        //        }
        //    }
        //    else
        //    {
        //        if (lv <= 25)
        //        {
        //            switch (grade)
        //            {
        //                case eGrade.Legend: return list_Legend.ToList();
        //                case eGrade.Set: return list_SetUnder25.ToList();
        //                default: return list_Normal.ToList();
        //            }
        //        }
        //        else
        //        {
        //            switch (grade)
        //            {
        //                case eGrade.Legend: return list_Legend.ToList();
        //                case eGrade.Set: return list_SetUnder70.ToList();
        //                default: return list_Normal.ToList();
        //            }
        //        }
        //    }
        //}

        //public bool Load()
        //{
        //    if (false == LoadOptionNormal()) return false;
        //    if (false == LoadOptionUnder25()) return false;
        //    if (false == LoadOptionUnder70()) return false;
        //    if (false == LoadOptionLegend()) return false;
        //    if (false == LoadOptionJewel()) return false;
        //    if (false == LoadOptionJewelLegend()) return false;

        //    return true;
        //}

        //private bool LoadOptionNormal()
        //{
        //    list_Normal.Clear();
        //    list_Normal.Add(eOption.CriRate);
        //    list_Normal.Add(eOption.CriDamageRate);
        //    list_Normal.Add(eOption.ResistAll);
        //    list_Normal.Add(eOption.ResistFire);
        //    list_Normal.Add(eOption.ResistIce);
        //    list_Normal.Add(eOption.ResistNature);
        //    list_Normal.Add(eOption.ResistNone);
        //    list_Normal.Add(eOption.EDFire);
        //    list_Normal.Add(eOption.EDIce);
        //    list_Normal.Add(eOption.EDNature);
        //    list_Normal.Add(eOption.EDNone);
        //    list_Normal.Add(eOption.EDRateFire);
        //    list_Normal.Add(eOption.EDRateIce);
        //    list_Normal.Add(eOption.EDRateNature);
        //    list_Normal.Add(eOption.EDRateNone);
        //    list_Normal.Add(eOption.DEF);
        //    list_Normal.Add(eOption.HP);
        //    list_Normal.Add(eOption.BWDMin);
        //    list_Normal.Add(eOption.BWDMax);
        //    list_Normal.Add(eOption.WD);
        //    list_Normal.Add(eOption.WDRate);
        //    //list_Normal.Add(eOption.AS);
        //    list_Normal.Add(eOption.ASRate);
        //    list_Normal.Add(eOption.ItemDropRate);
        //    list_Normal.Add(eOption.GoldDropRate);
        //    list_Normal.Add(eOption.Recovery);
        //    list_Normal.Add(eOption.FindMagicItemRate);


        //    list_Normal.Add(eOption.EpCriRate);
        //    list_Normal.Add(eOption.EpCriDamageRate);
        //    list_Normal.Add(eOption.EpResistAll);
        //    list_Normal.Add(eOption.EpResistFire);
        //    list_Normal.Add(eOption.EpResistIce);
        //    list_Normal.Add(eOption.EpResistNature);
        //    list_Normal.Add(eOption.EpResistNone);
        //    list_Normal.Add(eOption.EpEDFire);
        //    list_Normal.Add(eOption.EpEDIce);
        //    list_Normal.Add(eOption.EpEDNature);
        //    list_Normal.Add(eOption.EpEDNone);
        //    list_Normal.Add(eOption.EpEDRateFire);
        //    list_Normal.Add(eOption.EpEDRateIce);
        //    list_Normal.Add(eOption.EpEDRateNature);
        //    list_Normal.Add(eOption.EpEDRateNone);
        //    list_Normal.Add(eOption.EpDEF);
        //    list_Normal.Add(eOption.EpHP);
        //    list_Normal.Add(eOption.EpBWDMin);
        //    list_Normal.Add(eOption.EpBWDMax);
        //    list_Normal.Add(eOption.EpWD);
        //    list_Normal.Add(eOption.EpWDRate);
        //    //list_Normal.Add(eOption.EpAS);
        //    list_Normal.Add(eOption.EpASRate);
        //    list_Normal.Add(eOption.EpItemDropRate);
        //    list_Normal.Add(eOption.EpGoldDropRate);
        //    list_Normal.Add(eOption.EpRecovery);
        //    list_Normal.Add(eOption.EpFindMagicItemRate);

        //    return true;
        //}

        //private bool LoadOptionUnder25()
        //{
        //    list_SetUnder25.Clear();
        //    list_SetUnder25.Add(eOption.SETLuck);
        //    list_SetUnder25.Add(eOption.SETFindMagicItemRate);
        //    list_SetUnder25.Add(eOption.SETFire);
        //    list_SetUnder25.Add(eOption.SETIce);
        //    list_SetUnder25.Add(eOption.SETNature);
        //    list_SetUnder25.Add(eOption.SETNone);
        //    list_SetUnder25.Add(eOption.SETExtraStone);

        //    return true;
        //}

        //private bool LoadOptionUnder70()
        //{
        //    list_SetUnder70.Clear();
        //    list_SetUnder70.Add(eOption.SETAttack);
        //    list_SetUnder70.Add(eOption.SETLuck);
        //    list_SetUnder70.Add(eOption.SETFindMagicItemRate);
        //    list_SetUnder70.Add(eOption.SETExpRate);
        //    list_SetUnder70.Add(eOption.SETFire);
        //    list_SetUnder70.Add(eOption.SETIce);
        //    list_SetUnder70.Add(eOption.SETNature);
        //    list_SetUnder70.Add(eOption.SETNone);
        //    list_SetUnder70.Add(eOption.SETThorn);
        //    list_SetUnder70.Add(eOption.SETPoison);
        //    list_SetUnder70.Add(eOption.SETExtraStone);
        //    list_SetUnder70.Add(eOption.SETRecovery);
        //    list_SetUnder70.Add(eOption.SETHP);
        //    list_SetUnder70.Add(eOption.SETBurn);
        //    list_SetUnder70.Add(eOption.SETFreeze);
        //    return true;
        //}

        //private bool LoadOptionLegend()
        //{
        //    //list_SetUnder40.Add(eOption.SETResistAllToHp);

        //    list_Legend.Clear();
        //    list_Legend.Add(eOption.ExtraAtkChance);
        //    list_Legend.Add(eOption.CrushingBlow);
        //    list_Legend.Add(eOption.PlusSetEffect);
        //    //list_Legend.Add(eOption.ExtraDMGToRareMon);
        //    list_Legend.Add(eOption.LegnendDMGReduceRate);

        //    return true;
        //}

        //private bool LoadOptionJewel()
        //{
        //    list_Jewel.Clear();
        //    list_Jewel.Add(eOption.CriRate);
        //    list_Jewel.Add(eOption.CriDamageRate);
        //    list_Jewel.Add(eOption.ResistAll);
        //    list_Jewel.Add(eOption.ResistFire);
        //    list_Jewel.Add(eOption.ResistIce);
        //    list_Jewel.Add(eOption.ResistNature);
        //    list_Jewel.Add(eOption.ResistNone);
        //    list_Jewel.Add(eOption.EDFire);
        //    list_Jewel.Add(eOption.EDIce);
        //    list_Jewel.Add(eOption.EDNature);
        //    list_Jewel.Add(eOption.EDNone);
        //    //list_Jewel.Add(eOption.EDRateFire);
        //    //list_Jewel.Add(eOption.EDRateIce);
        //    //list_Jewel.Add(eOption.EDRateNature);
        //    //list_Jewel.Add(eOption.EDRateNone);
        //    list_Jewel.Add(eOption.DEF);
        //    list_Jewel.Add(eOption.HP);
        //    //list_Jewel.Add(eOption.BWDMin);
        //    //list_Jewel.Add(eOption.BWDMax);
        //    //list_Jewel.Add(eOption.WD);
        //    //list_Jewel.Add(eOption.WDRate);
        //    //list_Jewel.Add(eOption.AS);
        //    list_Jewel.Add(eOption.ASRate);
        //    list_Jewel.Add(eOption.ItemDropRate);
        //    list_Jewel.Add(eOption.GoldDropRate);
        //    list_Jewel.Add(eOption.Recovery);
        //    list_Jewel.Add(eOption.FindMagicItemRate);

        //    list_Jewel.Add(eOption.DEFRate);
        //    list_Jewel.Add(eOption.HPRate);
        //    //list_Jewel.Add(eOption.RecoveryRate);


        //    list_Jewel.Add(eOption.EpCriRate);
        //    list_Jewel.Add(eOption.EpCriDamageRate);
        //    list_Jewel.Add(eOption.EpResistAll);
        //    list_Jewel.Add(eOption.EpResistFire);
        //    list_Jewel.Add(eOption.EpResistIce);
        //    list_Jewel.Add(eOption.EpResistNature);
        //    list_Jewel.Add(eOption.EpResistNone);
        //    //list_Jewel.Add(eOption.EpEDFire);
        //    //list_Jewel.Add(eOption.EpEDIce);
        //    //list_Jewel.Add(eOption.EpEDNature);
        //    //list_Jewel.Add(eOption.EpEDNone);
        //    //list_Jewel.Add(eOption.EpEDRateFire);
        //    //list_Jewel.Add(eOption.EpEDRateIce);
        //    //list_Jewel.Add(eOption.EpEDRateNature);
        //    //list_Jewel.Add(eOption.EpEDRateNone);
        //    list_Jewel.Add(eOption.EpDEF);
        //    list_Jewel.Add(eOption.EpHP);
        //    //list_Jewel.Add(eOption.EpBWDMin);
        //    //list_Jewel.Add(eOption.EpBWDMax);
        //    //list_Jewel.Add(eOption.EpWD);
        //    //list_Jewel.Add(eOption.EpWDRate);
        //    //list_Jewel.Add(eOption.EpAS);
        //    list_Jewel.Add(eOption.EpASRate);
        //    list_Jewel.Add(eOption.EpItemDropRate);
        //    list_Jewel.Add(eOption.EpGoldDropRate);
        //    list_Jewel.Add(eOption.EpRecovery);
        //    list_Jewel.Add(eOption.EpFindMagicItemRate);

        //    list_Jewel.Add(eOption.EpDEFRate);
        //    list_Jewel.Add(eOption.EpHPRate);
        //    //list_Jewel.Add(eOption.EpRecoveryRate);

        //    return true;
        //}

        //private bool LoadOptionJewelLegend()
        //{
        //    list_JewelLegend.Add(eOption.ExtraAtkChance);
        //    list_JewelLegend.Add(eOption.CrushingBlow);
        //    list_JewelLegend.Add(eOption.LegnendDMGReduceRate);

        //    //list_JewelLegend.Add(eOption.ExtraDMGToRareMon);
        //    list_JewelLegend.Add(eOption.LegnendThornRate);
        //    list_JewelLegend.Add(eOption.LegnendPoisonRate);
        //    list_JewelLegend.Add(eOption.LegnendBurnRate);
        //    list_JewelLegend.Add(eOption.LegnendFreezeRate);
        //    return true;
        //}
    }
}

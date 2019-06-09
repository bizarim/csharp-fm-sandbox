using fmCommon;
using fmLibrary;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    class fmOptListMythic
    {
        List<int> m_nLvRange = new List<int>();

        // 레벨
        Dictionary<eParts, List<eOption>> m_optList = new Dictionary<eParts, List<eOption>>();


        public bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataOption> dic = table.Find<fmDataOption>(eFmDataType.Option);
            if (null == dic) return false;

            //m_optList.Add(eParts.Weapon, new List<eOption>());
            //m_optList.Add(eParts.Necklace, new List<eOption>());
            //m_optList.Add(eParts.Ring, new List<eOption>());
            //m_optList.Add(eParts.Belt, new List<eOption>());
            //m_optList.Add(eParts.Gloves, new List<eOption>());
            //m_optList.Add(eParts.Pants, new List<eOption>());
            //m_optList.Add(eParts.Armor, new List<eOption>());
            //m_optList.Add(eParts.Head, new List<eOption>());
            //m_optList.Add(eParts.Jewel, new List<eOption>());

            foreach (var node in dic)
            {
                int lv = node.Value.m_nAppearLv;
                eOption opt = node.Value.m_eOption;
                eOptGrade optGrade = (eOptGrade)((int)opt >> 8);

                foreach (var beyond in node.Value.m_nArrBeyond)
                {
                    if ((eBeyond)beyond == eBeyond.Mythic)
                    {
                        int cnt = node.Value.m_nArrParts.Length;
                        for (int i = 0; i < cnt; ++i)
                        {
                            eParts parts = (eParts)node.Value.m_nArrParts[i];

                            if (false == m_optList.ContainsKey(parts))
                                m_optList.Add(parts, new List<eOption>());

                            m_optList[parts].Add(opt);

                        }
                    }
                }
            }

            return true;
        }

        public List<eOption> GetList(eParts parts)
        {
            if (false == m_optList.ContainsKey(parts))
            {
                Logger.Error("GetList {0}", parts);
                return m_optList[eParts.Weapon].ToList();
            }
            else
                return m_optList[parts].ToList();

        }


        //Dictionary<eParts, List<eOption>> m_dic = new Dictionary<eParts, List<eOption>>();

        //List<eOption> list_Mythic = new List<eOption>();
        //List<eOption> list_MythicJewel = new List<eOption>();

        //public List<eOption> GetList(eParts parts)
        //{
        //    if (parts == eParts.Jewel)
        //        return list_MythicJewel.ToList();
        //    else
        //        return list_Mythic.ToList();

        //}

        //public bool Load(fmDataTable table)
        //{
        //    //Dictionary<int, fmDataOption> dic = table.Find<fmDataOption>(eFmDataType.Option);
        //    //if (null == dic) return false;

        //    //foreach (var node in dic)
        //    //{
        //    //    foreach (var beyond in node.Value.m_nArrBeyond)
        //    //    {
        //    //        if ((eBeyond)beyond == eBeyond.Mythic)
        //    //        {
        //    //            int cnt = node.Value.m_nArrParts.Length;
        //    //            for (int i = 0; i < cnt; ++i)
        //    //            {
        //    //                eParts parts = (eParts)node.Value.m_nArrParts[i];

        //    //                if (false == m_dic.ContainsKey(parts))
        //    //                    m_dic.Add(parts, new List<eOption>());

        //    //                m_dic[parts].Add(node.Value.m_eOption);
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    if (false == LoadOpt()) return false;
        //    if (false == LoadOptJewel()) return false;

        //    return true;
        //}

        //private bool LoadOptJewel()
        //{
        //    list_MythicJewel.Clear();
        //    list_MythicJewel.Add(eOption.EpCriRate);
        //    list_MythicJewel.Add(eOption.EpCriDamageRate);
        //    list_MythicJewel.Add(eOption.EpResistAll);
        //    list_MythicJewel.Add(eOption.EpResistFire);
        //    list_MythicJewel.Add(eOption.EpResistIce);
        //    list_MythicJewel.Add(eOption.EpResistNature);
        //    list_MythicJewel.Add(eOption.EpResistNone);
        //    list_MythicJewel.Add(eOption.EpDEF);
        //    list_MythicJewel.Add(eOption.EpHP);
        //    list_MythicJewel.Add(eOption.EpASRate);
        //    list_MythicJewel.Add(eOption.EpItemDropRate);
        //    list_MythicJewel.Add(eOption.EpGoldDropRate);
        //    list_MythicJewel.Add(eOption.EpRecovery);
        //    list_MythicJewel.Add(eOption.EpFindMagicItemRate);

        //    list_MythicJewel.Add(eOption.EpDEFRate);
        //    list_MythicJewel.Add(eOption.EpHPRate);

        //    return true;
        //}

        //private bool LoadOpt()
        //{
        //    list_Mythic.Clear();
        //    list_Mythic.Add(eOption.EpCriRate);
        //    list_Mythic.Add(eOption.EpCriDamageRate);
        //    list_Mythic.Add(eOption.EpResistAll);
        //    list_Mythic.Add(eOption.EpResistFire);
        //    list_Mythic.Add(eOption.EpResistIce);
        //    list_Mythic.Add(eOption.EpResistNature);
        //    list_Mythic.Add(eOption.EpResistNone);
        //    list_Mythic.Add(eOption.EpEDFire);
        //    list_Mythic.Add(eOption.EpEDIce);
        //    list_Mythic.Add(eOption.EpEDNature);
        //    list_Mythic.Add(eOption.EpEDNone);
        //    list_Mythic.Add(eOption.EpEDRateFire);
        //    list_Mythic.Add(eOption.EpEDRateIce);
        //    list_Mythic.Add(eOption.EpEDRateNature);
        //    list_Mythic.Add(eOption.EpEDRateNone);
        //    list_Mythic.Add(eOption.EpDEF);
        //    list_Mythic.Add(eOption.EpHP);
        //    list_Mythic.Add(eOption.EpBWDMin);
        //    list_Mythic.Add(eOption.EpBWDMax);
        //    list_Mythic.Add(eOption.EpWD);
        //    list_Mythic.Add(eOption.EpWDRate);

        //    list_Mythic.Add(eOption.EpASRate);
        //    list_Mythic.Add(eOption.EpItemDropRate);
        //    list_Mythic.Add(eOption.EpGoldDropRate);
        //    list_Mythic.Add(eOption.EpRecovery);
        //    list_Mythic.Add(eOption.EpFindMagicItemRate);

        //    list_Mythic.Add(eOption.AcDMGToRareMonRate);
        //    list_Mythic.Add(eOption.AcAllResistRate);
        //    list_Mythic.Add(eOption.AcHPRate);
        //    list_Mythic.Add(eOption.AcRecoveryRate);

        //    return true;

        //}
    }
}

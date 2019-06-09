using fmCommon;
using fmLibrary;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    class fmOptListEnchAnci
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
                    if ((eBeyond)beyond == eBeyond.Ancient)
                    {
                        int cnt = node.Value.m_nArrParts.Length;
                        for (int i = 0; i < cnt; ++i)
                        {
                            eParts parts = (eParts)node.Value.m_nArrParts[i];

                            if(false == m_optList.ContainsKey(parts))
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

        //List<eOption> list_Ancient = new List<eOption>();
        //List<eOption> list_AncientJewel = new List<eOption>();

        //public List<eOption> GetList(eParts parts)
        //{
        //    if (parts == eParts.Jewel)
        //        return list_AncientJewel.ToList();
        //    else
        //        return list_Ancient.ToList();

        //}

        //public bool Load()
        //{
        //    if (false == LoadOpt()) return false;
        //    if (false == LoadOptJewel()) return false;

        //    return true;
        //}

        //private bool LoadOptJewel()
        //{
        //    list_AncientJewel.Clear();
        //    list_AncientJewel.Add(eOption.EpCriRate);
        //    list_AncientJewel.Add(eOption.EpCriDamageRate);
        //    list_AncientJewel.Add(eOption.EpResistAll);
        //    list_AncientJewel.Add(eOption.EpResistFire);
        //    list_AncientJewel.Add(eOption.EpResistIce);
        //    list_AncientJewel.Add(eOption.EpResistNature);
        //    list_AncientJewel.Add(eOption.EpResistNone);
        //    //list_AncientJewel.Add(eOption.EpEDFire);
        //    //list_AncientJewel.Add(eOption.EpEDIce);
        //    //list_AncientJewel.Add(eOption.EpEDNature);
        //    //list_AncientJewel.Add(eOption.EpEDNone);
        //    //list_AncientJewel.Add(eOption.EpEDRateFire);
        //    //list_AncientJewel.Add(eOption.EpEDRateIce);
        //    //list_AncientJewel.Add(eOption.EpEDRateNature);
        //    //list_AncientJewel.Add(eOption.EpEDRateNone);
        //    list_AncientJewel.Add(eOption.EpDEF);
        //    list_AncientJewel.Add(eOption.EpHP);
        //    //list_AncientJewel.Add(eOption.EpBWDMin);
        //    //list_AncientJewel.Add(eOption.EpBWDMax);
        //    //list_AncientJewel.Add(eOption.EpWD);
        //    //list_AncientJewel.Add(eOption.EpWDRate);
        //    //list_AncientJewel.Add(eOption.EpAS);
        //    list_AncientJewel.Add(eOption.EpASRate);
        //    list_AncientJewel.Add(eOption.EpItemDropRate);
        //    list_AncientJewel.Add(eOption.EpGoldDropRate);
        //    list_AncientJewel.Add(eOption.EpRecovery);
        //    list_AncientJewel.Add(eOption.EpFindMagicItemRate);

        //    list_AncientJewel.Add(eOption.EpDEFRate);
        //    list_AncientJewel.Add(eOption.EpHPRate);
        //    //list_AncientJewel.Add(eOption.EpRecoveryRate);

        //    return true;
        //}

        //private bool LoadOpt()
        //{
        //    list_Ancient.Clear();
        //    list_Ancient.Add(eOption.EpCriRate);
        //    list_Ancient.Add(eOption.EpCriDamageRate);
        //    list_Ancient.Add(eOption.EpResistAll);
        //    list_Ancient.Add(eOption.EpResistFire);
        //    list_Ancient.Add(eOption.EpResistIce);
        //    list_Ancient.Add(eOption.EpResistNature);
        //    list_Ancient.Add(eOption.EpResistNone);
        //    list_Ancient.Add(eOption.EpEDFire);
        //    list_Ancient.Add(eOption.EpEDIce);
        //    list_Ancient.Add(eOption.EpEDNature);
        //    list_Ancient.Add(eOption.EpEDNone);
        //    list_Ancient.Add(eOption.EpEDRateFire);
        //    list_Ancient.Add(eOption.EpEDRateIce);
        //    list_Ancient.Add(eOption.EpEDRateNature);
        //    list_Ancient.Add(eOption.EpEDRateNone);
        //    list_Ancient.Add(eOption.EpDEF);
        //    list_Ancient.Add(eOption.EpHP);
        //    list_Ancient.Add(eOption.EpBWDMin);
        //    list_Ancient.Add(eOption.EpBWDMax);
        //    list_Ancient.Add(eOption.EpWD);
        //    list_Ancient.Add(eOption.EpWDRate);
        //    //list_Ancient.Add(eOption.EpAS);
        //    list_Ancient.Add(eOption.EpASRate);
        //    list_Ancient.Add(eOption.EpItemDropRate);
        //    list_Ancient.Add(eOption.EpGoldDropRate);
        //    list_Ancient.Add(eOption.EpRecovery);
        //    list_Ancient.Add(eOption.EpFindMagicItemRate);

        //    list_Ancient.Add(eOption.AcDMGToRareMonRate);
        //    list_Ancient.Add(eOption.AcAllResistRate);
        //    list_Ancient.Add(eOption.AcHPRate);
        //    list_Ancient.Add(eOption.AcRecoveryRate);

        //    return true;

        //}
    }
}

using fmCommon;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    class fmOptListEnchNorm
    {
        List<eOption> list_Normal_GoldEnchant = new List<eOption>();
        List<eOption> list_LegendSet_GoldEnchant_under25 = new List<eOption>();
        List<eOption> list_LegendSet_GoldEnchant_under70 = new List<eOption>();

        public List<eOption> GetList(eGrade grade, int lv = 25)
        {
            if (lv <= 25)
            {
                switch (grade)
                {
                    case eGrade.Legend: return list_LegendSet_GoldEnchant_under25.ToList();
                    case eGrade.Set: return list_LegendSet_GoldEnchant_under25.ToList();
                    default: return list_Normal_GoldEnchant.ToList();
                }
            }
            else
            {
                switch (grade)
                {
                    case eGrade.Legend: return list_LegendSet_GoldEnchant_under70.ToList();
                    case eGrade.Set: return list_LegendSet_GoldEnchant_under70.ToList();
                    default: return list_Normal_GoldEnchant.ToList();
                }
            }

        }

        public bool Load()
        {
            if (false == LoadOptionNormalGoldEnchant()) return false;
            if (false == LoadOptionLegendSetGoldEnchantUnder25()) return false;
            if (false == LoadOptionLegendSetGoldEnchantUnder70()) return false;
            return true;
        }


        private bool LoadOptionNormalGoldEnchant()
        {
            list_Normal_GoldEnchant.Clear();
            list_Normal_GoldEnchant.Clear();
            list_Normal_GoldEnchant.Add(eOption.CriRate);
            list_Normal_GoldEnchant.Add(eOption.CriDamageRate);
            list_Normal_GoldEnchant.Add(eOption.ResistAll);
            list_Normal_GoldEnchant.Add(eOption.ResistFire);
            list_Normal_GoldEnchant.Add(eOption.ResistIce);
            list_Normal_GoldEnchant.Add(eOption.ResistNature);
            list_Normal_GoldEnchant.Add(eOption.ResistNone);
            list_Normal_GoldEnchant.Add(eOption.EDFire);
            list_Normal_GoldEnchant.Add(eOption.EDIce);
            list_Normal_GoldEnchant.Add(eOption.EDNature);
            list_Normal_GoldEnchant.Add(eOption.EDNone);
            list_Normal_GoldEnchant.Add(eOption.EDRateFire);
            list_Normal_GoldEnchant.Add(eOption.EDRateIce);
            list_Normal_GoldEnchant.Add(eOption.EDRateNature);
            list_Normal_GoldEnchant.Add(eOption.EDRateNone);
            list_Normal_GoldEnchant.Add(eOption.DEF);
            list_Normal_GoldEnchant.Add(eOption.HP);
            list_Normal_GoldEnchant.Add(eOption.BWDMin);
            list_Normal_GoldEnchant.Add(eOption.BWDMax);
            list_Normal_GoldEnchant.Add(eOption.WD);
            list_Normal_GoldEnchant.Add(eOption.WDRate);
            //list_Normal_GoldEnchant.Add(eOption.AS);
            list_Normal_GoldEnchant.Add(eOption.ASRate);
            list_Normal_GoldEnchant.Add(eOption.ItemDropRate);
            list_Normal_GoldEnchant.Add(eOption.GoldDropRate);
            list_Normal_GoldEnchant.Add(eOption.Recovery);
            list_Normal_GoldEnchant.Add(eOption.FindMagicItemRate);


            list_Normal_GoldEnchant.Add(eOption.EpCriRate);
            list_Normal_GoldEnchant.Add(eOption.EpCriDamageRate);
            list_Normal_GoldEnchant.Add(eOption.EpResistAll);
            list_Normal_GoldEnchant.Add(eOption.EpResistFire);
            list_Normal_GoldEnchant.Add(eOption.EpResistIce);
            list_Normal_GoldEnchant.Add(eOption.EpResistNature);
            list_Normal_GoldEnchant.Add(eOption.EpResistNone);
            list_Normal_GoldEnchant.Add(eOption.EpEDFire);
            list_Normal_GoldEnchant.Add(eOption.EpEDIce);
            list_Normal_GoldEnchant.Add(eOption.EpEDNature);
            list_Normal_GoldEnchant.Add(eOption.EpEDNone);
            list_Normal_GoldEnchant.Add(eOption.EpEDRateFire);
            list_Normal_GoldEnchant.Add(eOption.EpEDRateIce);
            list_Normal_GoldEnchant.Add(eOption.EpEDRateNature);
            list_Normal_GoldEnchant.Add(eOption.EpEDRateNone);
            list_Normal_GoldEnchant.Add(eOption.EpDEF);
            list_Normal_GoldEnchant.Add(eOption.EpHP);
            list_Normal_GoldEnchant.Add(eOption.EpBWDMin);
            list_Normal_GoldEnchant.Add(eOption.EpBWDMax);
            list_Normal_GoldEnchant.Add(eOption.EpWD);
            list_Normal_GoldEnchant.Add(eOption.EpWDRate);
            //list_Normal_GoldEnchant.Add(eOption.EpAS);
            list_Normal_GoldEnchant.Add(eOption.EpASRate);
            list_Normal_GoldEnchant.Add(eOption.EpItemDropRate);
            list_Normal_GoldEnchant.Add(eOption.EpGoldDropRate);
            list_Normal_GoldEnchant.Add(eOption.EpRecovery);
            list_Normal_GoldEnchant.Add(eOption.EpFindMagicItemRate);
            return true;
        }

        private bool LoadOptionLegendSetGoldEnchantUnder70()
        {
            list_LegendSet_GoldEnchant_under70.Clear();
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETAttack);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETLuck);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETFindMagicItemRate);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETExpRate);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETFire);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETIce);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETNature);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETNone);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETThorn);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETPoison);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETExtraStone);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETRecovery);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETHP);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETBurn);
            list_LegendSet_GoldEnchant_under70.Add(eOption.SETFreeze);
            list_LegendSet_GoldEnchant_under70.Add(eOption.ExtraAtkChance);
            list_LegendSet_GoldEnchant_under70.Add(eOption.CrushingBlow);
            list_LegendSet_GoldEnchant_under70.Add(eOption.PlusSetEffect);
            list_LegendSet_GoldEnchant_under70.Add(eOption.LegnendDMGReduceRate);

            return true;
        }

        private bool LoadOptionLegendSetGoldEnchantUnder25()
        {
            list_LegendSet_GoldEnchant_under25.Clear();
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETLuck);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETFindMagicItemRate);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETExpRate);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETFire);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETIce);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETNature);
            list_LegendSet_GoldEnchant_under25.Add(eOption.SETNone);
            list_LegendSet_GoldEnchant_under25.Add(eOption.ExtraAtkChance);
            list_LegendSet_GoldEnchant_under25.Add(eOption.CrushingBlow);
            list_LegendSet_GoldEnchant_under25.Add(eOption.PlusSetEffect);
            list_LegendSet_GoldEnchant_under25.Add(eOption.LegnendDMGReduceRate);
            return true;
        }
    }
}

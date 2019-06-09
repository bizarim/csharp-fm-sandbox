using fmCommon;
using fmServerCommon;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        public eErrorCode ChangeBaseOpt(ref rdItem changeItem)
        {
            changeItem.BaseOpt.Clear();
            GetBaseOpt(changeItem);

            return eErrorCode.Success;
        }

        public eErrorCode ChangeLegendSetOpt(ref rdItem changeItem)
        {
            // 기존 옵션 제거
            rdOption option = changeItem.AddOpts.Find(x => x.Grade == eOptGrade.Legend || x.Grade == eOptGrade.Set);
            if (null == option)
                return eErrorCode.Error;

            int hit = m_random.Next(0, 2);
            if (hit == 0)
                changeItem.Grade = eGrade.Legend;
            else
                changeItem.Grade = eGrade.Set;

            eOption kind = GetLegendSetOption(changeItem.Grade, changeItem.Parts, changeItem.Lv);

            option.Kind = kind;
            option.Grade = GetOptGrade(kind);
            option.Value = GetValue(changeItem.Lv, kind);

            return eErrorCode.Success;
        }

        //public bool GetEnchantList(ref rdItem enchantItem, out List<eOption> list)
        //{
        //    list = new List<eOption>();
        //    list.Clear();

        //    if (enchantItem.Grade < eGrade.Epic)
        //    {
        //        List<eOption> temp = GetEnchantNormOptListOnGold(enchantItem.Grade, enchantItem.Lv);

        //        foreach (var node in enchantItem.AddOpts)
        //        {
        //            eOption nodekind = node.Kind;
        //            temp.Remove(nodekind);
        //            eOptGrade nodecc = (eOptGrade)((int)nodekind >> 8);

        //            if (nodecc == eOptGrade.Normal)
        //            {
        //                eOption remove = (eOption)((int)nodekind + ((int)eOptGrade.Normal << 8));
        //                temp.Remove(remove);
        //            }
        //            else if (nodecc == eOptGrade.Epic)
        //            {
        //                eOption remove = (eOption)((int)nodekind - ((int)eOptGrade.Normal << 8));
        //                temp.Remove(remove);
        //            }
        //        }

        //        int maxcnt = 10;

        //        for (int i = 0; i < maxcnt; ++i)
        //        {
        //            int hit = m_random.Next(0, temp.Count);

        //            eOption kind = temp.ElementAt(hit);
        //            list.Add(kind);

        //            eOptGrade cc = (eOptGrade)((int)kind >> 8);

        //            temp.RemoveAt(hit);
        //            if (cc == eOptGrade.Normal)
        //            {
        //                eOption remove = (eOption)((int)kind + ((int)eOptGrade.Normal << 8));
        //                temp.Remove(remove);
        //            }
        //            else if (cc == eOptGrade.Epic)
        //            {
        //                eOption remove = (eOption)((int)kind - ((int)eOptGrade.Normal << 8));
        //                temp.Remove(remove);
        //            }
        //            // eOptCategory

        //        }
        //    }
        //    else
        //    {
        //        List<eOption> temp = GetEnchantNormOptListOnGold(enchantItem.Grade, enchantItem.Lv);

        //        int maxcnt = 3;

        //        for (int i = 0; i < maxcnt; ++i)
        //        {
        //            int hit = m_random.Next(0, temp.Count);

        //            eOption kind = temp.ElementAt(hit);
        //            list.Add(kind);
        //            temp.RemoveAt(hit);
        //        }
        //    }

        //    return true;
        //}

        //public eErrorCode EnchantAddOpt(eFinance finance, eOption selectedOpt, ref rdItem enchantItem)
        //{
        //    //Logger.Debug("Start Grade: {0}, Count: {1}", remeltItem.Grade, remeltItem.AddOpts.Count);


        //    if (null == enchantItem)
        //        return eErrorCode.Auth_PleaseLogin;

        //    if (GetOptionCount(enchantItem.Grade) <= enchantItem.AddOpts.Count)
        //        return eErrorCode.Item_Reroll_OverOpt;


        //    int lastOpt = enchantItem.AddOpts.Count + 1;

        //    if (eGrade.Epic <= enchantItem.Grade)
        //    {
        //        eGrade grade = eGrade.None;
        //        //Logger.Debug("In LS Grade: {0}, Count: {1}", remeltItem.Grade, remeltItem.AddOpts.Count);
        //        eOptGrade optGrade = GetOptGrade(selectedOpt);

        //        if (optGrade == eOptGrade.None || optGrade == eOptGrade.Normal || optGrade == eOptGrade.Epic || optGrade == eOptGrade.Ancient)
        //            return eErrorCode.Item_Reroll_OverOpt;

        //        if (optGrade == eOptGrade.Set)
        //            grade = eGrade.Set;
        //        else if (optGrade == eOptGrade.Legend)
        //            grade = eGrade.Legend;

        //        rdOption option = new rdOption(lastOpt + 1, false, optGrade, selectedOpt, GetValue(enchantItem.Lv, selectedOpt));
        //        enchantItem.AddOpts.Add(option);
        //        enchantItem.Grade = grade;
        //    }
        //    else
        //    {
        //        eOptGrade optGrade = GetOptGrade(selectedOpt);
        //        if (optGrade == eOptGrade.Set || optGrade == eOptGrade.Legend)
        //            return eErrorCode.Item_Reroll_OverOpt;

        //        if (finance == eFinance.Ruby)
        //        {
        //            foreach (var node in enchantItem.AddOpts)
        //            {
        //                if (node.Kind == selectedOpt)
        //                    return eErrorCode.Item_AlreadySameOpt;
        //            }
        //        }

        //        rdOption option = new rdOption(lastOpt + 1, false, GetOptGrade(selectedOpt), selectedOpt, GetValue(enchantItem.Lv, selectedOpt));
        //        enchantItem.AddOpts.Add(option);
        //        enchantItem.Grade = GetGradeByAddOptCnt(enchantItem.AddOpts.Count);
        //    }
        //    //Logger.Debug("End Grade: {0}, Count: {1}", remeltItem.Grade, remeltItem.AddOpts.Count);

        //    return eErrorCode.Success;
        //}
    }
}

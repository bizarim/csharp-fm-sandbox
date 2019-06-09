using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        public eErrorCode CreateMythic(eOption selectOpt, eElement element, ref rdItem mythicItem)
        {
            rdOption keepOpt = null;

            foreach (var node in mythicItem.AddOpts)
            {
                if (eOptGrade.Legend == node.Grade || eOptGrade.Set == node.Grade)
                {
                    keepOpt = node;
                }
            }

            if (null == keepOpt)
                return eErrorCode.Error;

            int lv = mythicItem.Lv;
            eGrade grade = mythicItem.Grade;
            eParts parts = mythicItem.Parts;

            if (parts != eParts.Weapon)
            {
                if (selectOpt == eOption.None)
                    return eErrorCode.Params_InvalidParam;
            }

            int changeCode = 0;
            if (parts == eParts.Weapon)
            {
                if (false == ChangeWeaponImageCode(eBeyond.Mythic, mythicItem.Code, out changeCode))
                {
                    Logger.Error("Failed. CreateMythic {0} / {1} / {2}", parts, mythicItem.Code, selectOpt);
                    return eErrorCode.Server_TableError;
                }
            }
            else
            {
                if (false == ChangePartsImageCode(eBeyond.Mythic, mythicItem.Parts, selectOpt, out changeCode))
                {
                    Logger.Error("Failed. CreateMythic {0} / {1} / {2}", parts, mythicItem.Code, selectOpt);
                    return eErrorCode.Params_InvalidParam;
                }
            }

            mythicItem.Code = changeCode;
            mythicItem.Beyond = eBeyond.Mythic;
            mythicItem.AddOpts.Clear();
            mythicItem.BaseOpt.Clear();

            if (parts == eParts.Weapon)
            {
                float min = 0;
                float max = 0;
                float speed = 0;
                GetWeapon(lv, out min, out max, out speed);
                {
                    eOption select = eOption.BWDMin;
                    rdOption option = new rdOption(1, false, eOptGrade.Normal, select, min);
                    mythicItem.BaseOpt.Add(option);
                }
                {
                    eOption select = eOption.BWDMax;
                    rdOption option = new rdOption(2, false, eOptGrade.Normal, select, max);
                    mythicItem.BaseOpt.Add(option);
                }
                {
                    eOption select = eOption.AS;
                    rdOption option = new rdOption(3, false, eOptGrade.Normal, select, speed);
                    mythicItem.BaseOpt.Add(option);
                }
                {
                    eOption select = eOption.Element;
                    float value = (float)element;
                    rdOption option = new rdOption(4, false, eOptGrade.Normal, select, value);
                    mythicItem.BaseOpt.Add(option);
                }
            }
            else
            {
                float value = 0f;
                GetBaseValue(mythicItem.Lv, selectOpt, out value);
                rdOption option = new rdOption(1, false, eOptGrade.Normal, selectOpt, value);
                mythicItem.BaseOpt.Add(option);
            }

            List<eOption> temp = GetAncientOptList(parts);

            int cnt = GetOptionCount(grade, eBeyond.Mythic);

            for (int i = 0; i < cnt; ++i)
            {
                int hit = m_random.Next(0, temp.Count);

                eOption kind = temp.ElementAt(hit);

                rdOption option = new rdOption(i + 1, false, GetOptGrade(kind), kind, GetAncientValue(lv, kind));
                mythicItem.AddOpts.Add(option);

                temp.RemoveAt(hit);
            }

            {
                keepOpt.Index = mythicItem.AddOpts.Count + 1;
                mythicItem.AddOpts.Add(keepOpt);
            }

            return eErrorCode.Success;
        }

        //public eErrorCode AddOptToMythic(eOption selectedOpt, ref rdItem mythicItem)
        //{

        //    if (null == mythicItem)
        //        return eErrorCode.Auth_PleaseLogin;

        //    if (GetOptionCount(mythicItem.Grade, eBeyond.Mythic) <= mythicItem.AddOpts.Count)
        //        return eErrorCode.Item_Reroll_OverOpt;


        //    int lastOpt = mythicItem.AddOpts.Count + 1;

        //    if (eGrade.Epic <= mythicItem.Grade)
        //    {
        //        eGrade grade = eGrade.None;
        //        //Logger.Debug("In LS Grade: {0}, Count: {1}", remeltItem.Grade, remeltItem.AddOpts.Count);
        //        eOptGrade optGrade = GetOptGrade(selectedOpt);

        //        if (optGrade == eOptGrade.Set)
        //            grade = eGrade.Set;
        //        else if (optGrade == eOptGrade.Legend)
        //            grade = eGrade.Legend;

        //        rdOption option = new rdOption(lastOpt + 1, false, optGrade, selectedOpt, GetValue(mythicItem.Lv, selectedOpt));
        //        mythicItem.AddOpts.Add(option);
        //        mythicItem.Grade = grade;
        //    }
        //    else
        //    {
        //        foreach (var node in mythicItem.AddOpts)
        //        {
        //            if (node.Kind == selectedOpt)
        //                return eErrorCode.Item_AlreadySameOpt;
        //        }

        //        rdOption option = new rdOption(lastOpt + 1, false, GetOptGrade(selectedOpt), selectedOpt, GetValue(mythicItem.Lv, selectedOpt));
        //        mythicItem.AddOpts.Add(option);
        //        mythicItem.Grade = GetGradeByAddOptCnt(mythicItem.AddOpts.Count, eBeyond.Mythic);
        //    }

        //    return eErrorCode.Success;
        //}
    }
}

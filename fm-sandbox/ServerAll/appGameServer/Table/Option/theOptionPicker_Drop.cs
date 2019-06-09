using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        public void GetItem(fmDropItem fdi, out rdItem item)
        {

            int lv = fdi.Lv;
            eGrade grade = fdi.Grade;
            eParts parts = fdi.Parts;

            item = new rdItem();
            item.Lv = lv;
            item.Grade = grade;
            item.Parts = parts;
            item.BaseOpt = new List<rdOption>();
            item.AddOpts = new List<rdOption>();



            GetBaseOpt(item);


            List<eOption> temp = GetList(eGrade.Normal, parts, lv);

            int cnt = GetOptionCount(grade);

            for (int i = 0; i < cnt; ++i)
            {
                int hit = m_random.Next(0, temp.Count);

                eOption kind = temp.ElementAt(hit);
                eOptGrade cc = (eOptGrade)((int)kind >> 8);

                rdOption option = new rdOption(i + 1, false, GetOptGrade(kind), kind, GetValue(lv, kind));
                item.AddOpts.Add(option);
                temp.RemoveAt(hit);
                if (cc == eOptGrade.Normal)
                {
                    eOption remove = (eOption)((int)kind + ((int)eOptGrade.Normal << 8));
                    temp.Remove(remove);
                }
                else if (cc == eOptGrade.Epic)
                {
                    eOption remove = (eOption)((int)kind - ((int)eOptGrade.Normal << 8));
                    temp.Remove(remove);
                }
                // eOptCategory
            }

            if (eGrade.Legend <= grade)
            {
                eOptGrade optGrade = GetLegendSetOptGrade(grade);
                eOption kind = GetLegendSetOption(grade, parts, lv);
                Logger.Debug("GetItem GetLegendSetOption: {0}, {1},{2},{3}", kind, grade, parts, lv);
                rdOption option = new rdOption(cnt + 1, false, optGrade, kind, GetValue(lv, kind));
                item.AddOpts.Add(option);
            }

        }

        public void GetItemWithBoss(fmDropItem fdi, eOption selecedOpt, out rdItem item)
        {

            int lv = fdi.Lv;
            eGrade grade = fdi.Grade;
            eParts parts = fdi.Parts;
            
            item = new rdItem();
            item.Lv = lv;
            item.Grade = grade;
            item.Parts = parts;
            item.BaseOpt = new List<rdOption>();
            item.AddOpts = new List<rdOption>();



            GetBaseOpt(item);


            List<eOption> temp = GetList(eGrade.Normal, parts, lv);

            int cnt = GetOptionCount(grade);
            //Console.WriteLine(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                int hit = m_random.Next(0, temp.Count);

                eOption kind = temp.ElementAt(hit);
                eOptGrade cc = (eOptGrade)((int)kind >> 8);

                rdOption option = new rdOption(i + 1, false, GetOptGrade(kind), kind, GetValue(lv, kind));
                item.AddOpts.Add(option);
                temp.RemoveAt(hit);
                if (cc == eOptGrade.Normal)
                {
                    eOption remove = (eOption)((int)kind + ((int)eOptGrade.Normal << 8));
                    temp.Remove(remove);
                }
                else if (cc == eOptGrade.Epic)
                {
                    eOption remove = (eOption)((int)kind - ((int)eOptGrade.Normal << 8));
                    temp.Remove(remove);
                }
                // eOptCategory
            }

            if (eGrade.Legend <= grade)
            {
                eOptGrade optGrade = GetLegendSetOptGrade(grade);
                //Console.WriteLine(optGrade);
                eOption kind = selecedOpt;
                rdOption option = new rdOption(cnt + 1, false, optGrade, kind, GetValue(lv, kind));
                item.AddOpts.Add(option);
            }

        }
    }
}

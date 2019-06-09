using fmCommon;
using fmLibrary;
using System;
using System.Collections.Generic;

namespace appGameServer.Table.Gocha
{
    public class fmGochaRerollAddOpt
    {
        protected Dictionary<eGrade, int> m_dic = new Dictionary<eGrade, int>();

        public fmGochaRerollAddOpt()
        {
            m_dic.Clear();
            //m_dic.Add(eGrade.Normal , 100);     //  Normal    : 100%
            //m_dic.Add(eGrade.Magic  ,  80);     //  Magic     : 80%
            //m_dic.Add(eGrade.Rare   ,  50);     //  Rare      : 50%
            //m_dic.Add(eGrade.Epic   ,  20);     //  Epic      : 20%
            //m_dic.Add(eGrade.Legend ,   5);     //  Legend&Set: 5%

            m_dic.Add(eGrade.Normal ,   9999);     //  Normal    : 100%
            m_dic.Add(eGrade.Magic  ,   7999);     //  Magic     : 80%
            m_dic.Add(eGrade.Rare   ,   5999);     //  Rare      : 50%
            m_dic.Add(eGrade.Epic   ,   3999);     //  Epic      : 20%
            //m_dic.Add(eGrade.Legend ,   1999);     //  Legend&Set: 5%
        }

        public bool Gocha(eGrade grade, int hit)
        {
            if (false == m_dic.ContainsKey(grade))
                return false;

            //Logger.Debug("{0} / {1} / {2}", grade, hit, m_dic[grade]);

            if (hit < m_dic[grade])
                return true;

            return false;
        }
    }
}

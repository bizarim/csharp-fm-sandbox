//using fmCommon;
//using System.Collections.Generic;

//namespace appGameServer.Table
//{
//    public class theSetEffecter
//    {
//        private static Dictionary<eOption, List<fmDataSetEffect>> m_dic = new Dictionary<eOption, List<fmDataSetEffect>>();

//        public static bool Load(fmDataTable table)
//        {
//            Dictionary<int, fmDataSetEffect> dic = table.Find<fmDataSetEffect>(eFmDataType.SetEffect);

//            if (null == dic)
//                return false;

//            foreach (var node in dic)
//            {
//                if (false == m_dic.ContainsKey(node.Value.m_eSetOpt))
//                    m_dic.Add(node.Value.m_eSetOpt, new List<fmDataSetEffect>());

//                m_dic[node.Value.m_eSetOpt].Add(node.Value);
//            }

//            //m_dic = dic;

//            return true;
//        }

//        public static void Apply(ref fmStats equipStat)
//        {

//            int saCnt = equipStat.SETAttack + (int)equipStat.PlusSetEffect;
//            int slCnt = equipStat.SETLuck + (int)equipStat.PlusSetEffect;

//            {
//                foreach (var node in m_dic[eOption.SETAttack])
//                {
//                    if (node.m_nSetCnt < saCnt && saCnt <= node.m_nSetCnt)
//                    {
//                        equipStat.AddSetEffect(node);
//                    }
//                }
//            }
//            {
//                foreach (var node in m_dic[eOption.SETLuck])
//                {
//                    if (node.m_nSetCnt < saCnt && saCnt <= node.m_nSetCnt)
//                    {
//                        equipStat.AddSetEffect(node);
//                    }
//                }
//            }
//        }
//    }
//}

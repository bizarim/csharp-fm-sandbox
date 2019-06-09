//using fmCommon;
//using System;

//namespace appGameServer.Table
//{
//    public class fmGochaMap : fmGochaBoard
//    {
//        public fmGochaMap(Random ran)
//        {
//            m_random = ran;
//        }

//        public override void Add(fmData fd)
//        {
//            var data = fd as fmDataExplore;

//            int cnt = data.m_nArrDropMap.Length;
//            for (int i = 0; i < cnt; ++i)
//            {
//                int roll = 100 * m_nRate;

//                fmGochaNode node = new fmGochaNode();
//                node.m_fmData = null;

//                node.m_nGochaValue = data.m_nArrDropMap[i];

//                node.m_nBegin = m_nMaxVal;
//                node.m_nEnd = m_nMaxVal + roll;

//                m_listBoard.Add(node);

//                m_nMaxVal += roll;
//            }
//        }

//        public override fmData GochaData() { return null; }

//        public override int Gocha()
//        {
//            int hit = m_random.Next(m_nMinVal, m_nMaxVal);
//            //Console.WriteLine("hit: " + hit);
//            foreach (var node in m_listBoard)
//            {
//                if (node.m_nBegin <= hit && hit < node.m_nEnd)
//                    return node.m_nGochaValue;
//            }
//            return 0;
//        }
//    }
//}

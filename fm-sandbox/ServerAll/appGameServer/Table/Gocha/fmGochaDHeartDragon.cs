//using fmCommon;
//using System;
//using System.Collections.Generic;

//namespace appGameServer.Table
//{
//    public class fmGochaDHeartDragon
//    {
//        protected readonly int m_nRate = 1;
//        protected int m_nMinVal = 0;
//        protected int m_nMaxVal = 0;

//        protected Random m_random = null;
//        protected List<fmGochaNode> m_listBoard = new List<fmGochaNode>();

//        public fmGochaDHeartDragon(Random ran)
//        {
//            m_random = ran;
//        }

//        public void Add(fmDataDHeart data)
//        {
//            int cnt = data.m_nArrAppearDragon.Length;
//            for (int i = 0; i < cnt; ++i)
//            {
//                int roll = data.m_nArrAppearRateDragon[i] * m_nRate;

//                fmGochaNode node = new fmGochaNode();
//                node.m_fmData = null;

//                node.m_nGochaValue = data.m_nArrAppearDragon[i];

//                node.m_nBegin = m_nMaxVal;
//                node.m_nEnd = m_nMaxVal + roll;

//                m_listBoard.Add(node);

//                m_nMaxVal += roll;
//            }
//        }

//        public int Gocha()
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

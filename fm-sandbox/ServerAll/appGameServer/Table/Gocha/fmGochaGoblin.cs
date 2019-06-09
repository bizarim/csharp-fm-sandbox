//using fmCommon;
//using System;
//using System.Collections.Generic;

//namespace appGameServer.Table
//{
//    public class fmGochGolinNode
//    {
//        //public fmData m_fmData = null;

//        public int m_nGochaValue = 0;
//        public int m_nMonCode = 0;

//        public int m_nBegin = 0;
//        public int m_nEnd = 0;
//    }

//    public class fmGochaGoblin
//    {
//        protected readonly int m_nRate = 1;
//        protected int m_nMinVal = 0;
//        protected int m_nMaxVal = 0;

//        protected Random m_random = null;
//        protected List<fmGochGolinNode> m_listBoard = new List<fmGochGolinNode>();

//        public fmGochaGoblin(Random ran)
//        {
//            m_random = ran;
//        }

//        public void Add(fmDataGoblin data)
//        {
//            int cnt = data.m_nArrGoblin.Length;

//            for (int i = 0; i < cnt; ++i)
//            {
//                int roll = data.m_nArrDropRate[i] * m_nRate;

//                fmGochGolinNode node = new fmGochGolinNode();

//                node.m_nGochaValue = data.m_nArrDropKind[i];
//                node.m_nMonCode = data.m_nArrGoblin[i];

//                //Console.WriteLine("node.m_nMonCode: " + node.m_nMonCode);

//                node.m_nBegin = m_nMaxVal;
//                node.m_nEnd = m_nMaxVal + roll;

//                m_listBoard.Add(node);

//                m_nMaxVal += roll;
//            }



//        }

//        public bool Gocha(out int dropKind, out int monCode)
//        {
//            dropKind = 0;
//            monCode = 0;
//            int hit = m_random.Next(m_nMinVal, m_nMaxVal);

//            foreach (var node in m_listBoard)
//            {
//                if (node.m_nBegin <= hit && hit < node.m_nEnd)
//                {
//                    dropKind = node.m_nGochaValue;
//                    monCode = node.m_nMonCode;
//                    return true;
//                }
//            }
//            return false;
//        }
//    }
//}

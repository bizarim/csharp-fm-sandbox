using fmCommon;
using System;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class fmGochaDrop
    {
        protected readonly int m_nRate = 100;
        protected int m_nMinVal = 0;
        protected int m_nMaxVal = 0;

        public int Max { get { return m_nMaxVal; } }
        public int Min { get { return m_nMinVal; } }

        Dictionary<eReward, int> m_kinds = new Dictionary<eReward, int>();

        protected List<fmGochaNode> m_listBoard = new List<fmGochaNode>();

        public fmGochaDrop()
        {
            m_kinds.Clear();
            m_kinds.Add(eReward.None, 1200);
            m_kinds.Add(eReward.Gold, 4900);
            m_kinds.Add(eReward.Item, 2000);
            m_kinds.Add(eReward.Stone, 800);
            m_kinds.Add(eReward.Ruby, 1000);
            m_kinds.Add(eReward.SCKey, 100);

            m_nMinVal = (m_kinds[eReward.None] + m_kinds[eReward.Gold]) * m_nRate + (int)(m_kinds[eReward.Item] * m_nRate * 0.2);

            for (int i = 0; i < m_kinds.Count; ++i)
            {
                int roll = m_kinds[(eReward)i] * m_nRate;

                fmGochaNode node = new fmGochaNode();
                node.m_fmData = null;

                node.m_nGochaValue = i;

                node.m_nBegin = m_nMaxVal;
                node.m_nEnd = m_nMaxVal + roll;

                m_listBoard.Add(node);

                m_nMaxVal += roll;
            }
        }

        public eReward Gocha(int hit)
        {
            foreach (var node in m_listBoard)
            {
                if (node.m_nBegin <= hit && hit < node.m_nEnd)
                    return (eReward)node.m_nGochaValue;
            }

            return eReward.Item;
        }
    }
}

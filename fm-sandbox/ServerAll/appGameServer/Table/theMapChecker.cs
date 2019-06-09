using fmCommon;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class theMapChecker : Singleton<theMapChecker>
    {
        // 인던 번호 -> 거점 번호
        Dictionary<int, fmDataExplore> m_dic = new Dictionary<int, fmDataExplore>();

        public bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataExplore> dic = table.Find<fmDataExplore>(eFmDataType.Explore);
            if (null == dic)
                return false;

            foreach (var node in dic)
            {
                m_dic.Add(node.Value.m_nLinkCode, node.Value);
            }

            //m_dic = dic;

            return true;
        }

        public bool GetNextCode(int curMap, out int nextCode)
        {
            nextCode = 0;

            if (false == m_dic.ContainsKey(curMap))
                return false;

            nextCode = m_dic[curMap].m_nNextCode;

            return true;
        }
    }
}

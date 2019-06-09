using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class theInDunChecker : Singleton<theInDunChecker>
    {
        // 인던 번호 -> 거점 번호
        Dictionary<int, Dictionary<int, fmDataInDun>> m_dic = new Dictionary<int, Dictionary<int, fmDataInDun>>();

        public bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataInDun> dic = table.Find<fmDataInDun>(eFmDataType.InDun);
            if (null == dic)
                return false;

            foreach (var node in dic)
            {
                if (false == m_dic.ContainsKey(node.Value.m_nInDunCode))
                    m_dic.Add(node.Value.m_nInDunCode, new Dictionary<int, fmDataInDun>());

                m_dic[node.Value.m_nInDunCode].Add(node.Value.m_nPlace, node.Value);
            }

            return true;
        }

        public bool CanMove(int indunCode, int curPlace, int movePlace)
        {
            //Logger.Debug("CanMove {0}/{1}/{2}", indunCode, curPlace, movePlace);

            if (false == m_dic.ContainsKey(indunCode))
                return false;

            if (curPlace == 0 && movePlace == 1)
                return true;

            if (false == m_dic[indunCode].ContainsKey(curPlace))
                return false;

            int cnt = m_dic[indunCode][curPlace].m_nArrNextPlace.Length;

            for (int i = 0; i < cnt; ++i)
            {
                if (movePlace == m_dic[indunCode][curPlace].m_nArrNextPlace[i])
                    return true;
            }

            return false;
        }

        public bool TryGetRound(int indunCode, int movePlace, out int round, out int indunLv, out int boss, out int goblin)
        {
            round = 0;
            indunLv = 0;
            boss = 0;
            goblin = 0;

            if (false == m_dic.ContainsKey(indunCode)) return false;
            if (false == m_dic[indunCode].ContainsKey(movePlace)) return false;

            round = m_dic[indunCode][movePlace].m_nRound;
            indunLv = m_dic[indunCode][movePlace].m_nLv;
            boss = m_dic[indunCode][movePlace].m_nBoss;
            goblin = m_dic[indunCode][movePlace].m_nGoblin;

            return true;
        }

        public bool CanUseForge(int indunCode, int curPlace)
        {
            if (false == m_dic.ContainsKey(indunCode))
                return false;

            if (false == m_dic[indunCode].ContainsKey(curPlace))
                return false;

            if (0 == m_dic[indunCode][curPlace].m_nForge)
                return false;

            return true;
        }

        public bool IsLastPlace(int indunCode, int place)
        {
            if (false == m_dic.ContainsKey(indunCode))
                return false;

            if (false == m_dic[indunCode].ContainsKey(place))
                return false;

            int cnt = m_dic[indunCode][place].m_nArrNextPlace.Length;

            for (int i = 0; i < cnt; ++i)
            {
                if (0 != m_dic[indunCode][place].m_nArrNextPlace[i])
                    return false;
            }

            return true;
        }

        public eOption GetDropOptionWithBoss(Random lordRandom, int indunCode, int place)
        {
            if (false == m_dic.ContainsKey(indunCode))
                return eOption.None;

            if (false == m_dic[indunCode].ContainsKey(place))
                return eOption.None;

            if(0 == m_dic[indunCode][place].m_nBoss)
                return eOption.None;

            int cnt = m_dic[indunCode][place].m_nArrDropOption.Length;

            int hit = lordRandom.Next(0, cnt);

            return (eOption)m_dic[indunCode][place].m_nArrDropOption[hit];
        }
    }
}

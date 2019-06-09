using fmCommon;
using System;
using System.Linq;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public static class theMissionPicker
    {
        static Random m_random = new Random();
        static Dictionary<int, fmDataMission> m_dicMission = null;

        public static bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataMission> dic = table.Find<fmDataMission>(eFmDataType.Mission);
            if (null == dic)
                return false;

            m_dicMission = dic;
            return true;
        }

        public static bool TryGet(out List<rdMission> missions)
        {
            missions = new List<rdMission>();

            List<fmDataMission> temp = m_dicMission.Values.ToList();

            for (int i = 0; i < theGameConst.MaxMissionCnt; ++i)
            {
                int hit = m_random.Next(0, temp.Count);

                fmDataMission data = temp.ElementAt(hit);
                //if (null == data)
                //    return false;

                missions.Add(new rdMission
                {
                    Type = data.m_eMission,
                    Code = data.Code,
                    Condition = 0,
                    Complete = false
                });


                temp.RemoveAt(hit);
            }

            return true;
        }
    }
}

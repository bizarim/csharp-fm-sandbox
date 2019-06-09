using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_GetLord : UserRedisQuery
    {
        public long i_biAccID = 0;
        public fmLord o_lord = null;

        public urq_GetLord(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            var db = GetDatabase();

            if (false == db.IsExistsLord(i_biAccID))
                return eErrorCode.Lord_NoneExist;

            o_lord = new fmLord();
            o_lord.ActTime = fmServerTime.Now;
            o_lord.State = eLordState.Login;

            try
            {
                o_lord.AccId = i_biAccID;

                fmLordBase lordInfo = null;

                rdStat lordStat = null;
                List<rdItem> items = null;
                List<rdMission> missions = null;
                List<rdMap> maps = null;
                fmMissionBase missionBase = null;
                List<rdInDun> inDuns = null;

                db.GetLordBase(i_biAccID, out lordInfo);
                db.GetLordStat(i_biAccID, out lordStat);
                db.GetLordItems(i_biAccID, out items);
                db.GetMissions(i_biAccID, out missions);
                db.GetMaps(i_biAccID, out maps);
                db.GetMissionBase(i_biAccID, out missionBase);
                db.GetLordInDuns(i_biAccID, out inDuns);

                o_lord.InitLordBase(lordInfo);
                o_lord.InitStat(lordStat);
                o_lord.InitItems(items);

            }
            catch (Exception ex)
            {
                Logger.Error("accid:{0}, ex:{1}", i_biAccID, ex.ToString());
                return eErrorCode.Query_Fail;
            }

            if (true == o_lord.Block)
                return eErrorCode.Server_Block;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            o_lord = null;
        }
    }
}

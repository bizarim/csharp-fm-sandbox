using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_SetMissions : UserRedisQuery
    {
        public long i_biAccID = 0;
        public fmLordBase       i_lordbase = null;
        public List<rdMission>  i_missions = null;
        public fmMissionBase    i_missionBase = null;

        public urq_SetMissions(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {

            if (i_biAccID == 0 || i_missions == null || i_lordbase == null || i_missionBase == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            var trans = db.CreateTransaction();

            trans.SetLordBase(i_biAccID, i_lordbase);
            trans.SetMissions(i_biAccID, i_missions);
            trans.SetMissionBase(i_biAccID, i_missionBase);

            if (false == trans.Execute())
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            i_lordbase = null;
            i_missions = null;
            i_missionBase = null;
        }
    }
}

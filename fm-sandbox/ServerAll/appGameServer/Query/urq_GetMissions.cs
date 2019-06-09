using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_GetMissions : UserRedisQuery
    {
        public long i_biAccID = 0;
        public List<rdMission> o_missions = null;

        public urq_GetMissions(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.GetMissions(i_biAccID, out o_missions))
                return eErrorCode.Qeury_NoneKey;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            o_missions = null;
        }
    }
}

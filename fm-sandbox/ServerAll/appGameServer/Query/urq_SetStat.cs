using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_SetStat : UserRedisQuery
    {
        public long i_biAccID = 0;
        public rdStat i_rdStat = null;

        public urq_SetStat(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || i_rdStat == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.SetLordStat(i_biAccID, i_rdStat))
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            i_rdStat = null;
        }
    }
}

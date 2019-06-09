using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_GetRank : UserRedisQuery
    {
        public fmRankerKey      i_rankerKey = null;
        public List<fmRanker>   o_rankers = null;
        public long             o_myRank = 0;

        public urq_GetRank(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_rankerKey == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.GetMazeTopRank(out o_rankers))
                return eErrorCode.Qeury_NoneKey;

            if (false == db.GetMyMazeRank(i_rankerKey, out o_myRank))
                return eErrorCode.Qeury_NoneKey;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_rankerKey = null;
            o_rankers = null;
            o_myRank = 0;
        }
    }
}

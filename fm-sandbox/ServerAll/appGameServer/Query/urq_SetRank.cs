using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    // user redis query

    public class urq_SetRank : UserRedisQuery
    {
        public long i_biAccID = 0;
        public string i_strName = string.Empty;
        public int i_nFloor = 0;

        public urq_SetRank(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || true == string.IsNullOrEmpty(i_strName) || i_nFloor == 0)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.SetMazeRank(new fmRankerKey { AccId = i_biAccID, Name = i_strName }, i_nFloor))
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            i_strName = string.Empty;
            i_nFloor = 0;
        }
    }
}

using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    // user redis query

    public class urq_SetLordBase : UserRedisQuery
    {
        public long i_biAccID = 0;
        public fmLordBase i_lordBase = null;

        public urq_SetLordBase(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || i_lordBase == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.SetLordBase(i_biAccID, i_lordBase))
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_lordBase = null;
        }
    }
}


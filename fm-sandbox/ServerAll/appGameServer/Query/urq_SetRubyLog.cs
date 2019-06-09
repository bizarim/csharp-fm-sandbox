using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    public class urq_SetRubyLog : UserRedisQuery
    {
        public rdRubyLog i_rdLog = null;

        public urq_SetRubyLog(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_rdLog == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.SetRubyLog(i_rdLog.AccId, i_rdLog))
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_rdLog = null;
        }
    }
}

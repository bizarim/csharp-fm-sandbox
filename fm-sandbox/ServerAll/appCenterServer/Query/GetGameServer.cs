using fmCommon;
using fmServerCommon;

namespace appCenterServer.Query
{
    // user redis query

    public class urq_GetGameServer : UserRedisQuery
    {
        public int o_gameSvr = 0;
        public long i_biAccID = 0;

        public urq_GetGameServer(eRedis database)
        {
            m_eDataBase = database;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID <= 0)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.GetGameServer(i_biAccID, out o_gameSvr))
                return eErrorCode.Auth_InvalidToken;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            o_gameSvr = 0;
            i_biAccID = 0;
        }
    }
}

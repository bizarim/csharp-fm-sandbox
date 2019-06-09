using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    // user redis query

    public class urq_GetToken : UserRedisQuery
    {
        public string i_strToken = string.Empty;
        public long o_biAccID = 0;

        public urq_GetToken(eRedis database)
        {
            m_eDataBase = database;
        }

        public override eErrorCode Execute()
        {
            if (true == string.IsNullOrEmpty(i_strToken))
                return eErrorCode.Auth_InvalidToken;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.GetAuthToken(i_strToken, out o_biAccID))
                return eErrorCode.Auth_InvalidToken;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_strToken = string.Empty;
            o_biAccID = 0;
        }
    }
}

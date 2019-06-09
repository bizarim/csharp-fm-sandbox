using fmCommon;
using fmServerCommon;

namespace appAuthServer
{
    // user redis query

    /// <summary>
    /// 인증 토큰 업데이트
    /// </summary>
    public class urq_UpdateAuthToken : UserRedisQuery
    {
        public long i_biAccID = 0;
        public int i_gameServer = 0;
        public string i_strToken = string.Empty;

        public urq_UpdateAuthToken(eRedis database)
        {
            m_eDataBase = database;
        }

        public override eErrorCode Execute()
        {
            var db = GetDatabase();

            if (i_biAccID <= 0 || string.IsNullOrEmpty(i_strToken))
                return eErrorCode.Query_Params;

            db.UpdateAuthToken(i_strToken, i_biAccID, i_gameServer);

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            i_gameServer = 0;
            i_strToken = string.Empty;
        }
    }
}

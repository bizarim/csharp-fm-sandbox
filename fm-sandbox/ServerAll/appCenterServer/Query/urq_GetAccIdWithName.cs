using fmCommon;
using fmServerCommon;

// user redis query
namespace appCenterServer.Query
{
    /// <summary>
    /// 닉네임 얻어오기
    /// </summary>
    public class urq_GetAccIdWithName : UserRedisQuery
    {
        public long o_biAccId = 0;
        public string i_strName = string.Empty;

        public urq_GetAccIdWithName(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (true == string.IsNullOrEmpty(i_strName))
                return eErrorCode.Query_Params;

            o_biAccId = GetAccIdWithName(i_strName);
            if (0 == o_biAccId)
                return eErrorCode.Lord_NoneExist;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            o_biAccId = 0;
            i_strName = string.Empty;
        }
    }
}
using fmServerCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace appAuthServer
{
    /// <summary>
    /// 유저 로그인
    /// </summary>
    public class usp_Login : MySqlQuery
    {
        public string   i_strUniqueKey = string.Empty;
        public int      i_nAppOs = 0;
        public long     o_biAccID = 0;
        public int      o_nResult = 999;

        public usp_Login(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "usp_Login";
            m_eCommandType = CommandType.StoredProcedure;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;

            command.Parameters.AddWithValue("@i_strUniqueKey", i_strUniqueKey);
            command.Parameters.AddWithValue("@i_nAppOs", i_nAppOs);
            command.Parameters.Add("@o_biAccID", MySqlDbType.Int64).Direction = ParameterDirection.Output;
            command.Parameters.Add("@o_nResult", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            o_nResult = Convert.ToInt32(m_mySqlCommand.Parameters["@o_nResult"].Value);
            o_biAccID = Convert.ToInt64(m_mySqlCommand.Parameters["@o_biAccID"].Value);
        }

        protected override void FreeParams()
        {
            i_strUniqueKey = string.Empty;
            o_biAccID = 0;
            o_nResult = 999;
        }
    }
}

using fmServerCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace appAuthServer
{
    /// <summary>
    /// 테스트
    /// </summary>
    public class usp_Test : MySqlQuery
    {
        public usp_Test(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "SELECT * FROM tbl_account;";
            m_eCommandType = CommandType.Text;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
        }

        protected override void FreeParams()
        {
        }
    }
}

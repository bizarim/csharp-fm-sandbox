using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_GetOtherLord : UserRedisQuery
    {
        private long m_biAccId = 0;
        public string i_strName = string.Empty;

        public fmLordBase o_lordBase = null;
        public List<rdItem> o_items = null;

        public urq_GetOtherLord(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            m_biAccId = GetAccIdWithName(i_strName);
            if (0 == m_biAccId)
                return eErrorCode.Lord_NoneExist;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            db.GetLordBase(m_biAccId, out o_lordBase);
            db.GetLordItems(m_biAccId, out o_items);

            return eErrorCode.Success;
        }

        public override void Release()
        {
            m_biAccId = 0;
            i_strName = string.Empty;
        }
    }
}

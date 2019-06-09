using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_EquipItem : UserRedisQuery
    {
        public long i_biAccID = 0;
        public List<rdItem> i_items = null;

        public urq_EquipItem(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || i_items == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            if (false == db.SetLordItems(i_biAccID, i_items))
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            //i_dsBatleState = null;
        }
    }
}

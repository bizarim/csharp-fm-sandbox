using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_BuyGood : UserRedisQuery
    {
        public long i_biAccID = 0;
        public fmLordBase i_lordInfo = null;
        public rdStat i_lordStat = null;
        public List<rdItem> i_items = null;

        public urq_BuyGood(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0
                || i_lordInfo == null || i_items == null || i_lordStat == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            var trans = db.CreateTransaction();
            trans.SetLordBase(i_biAccID, i_lordInfo);
            trans.SetLordStat(i_biAccID , i_lordStat);
            trans.SetLordItems(i_biAccID, i_items);

            if (false == trans.Execute())
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            //i_dsBatleState = null;
        }
    }
}

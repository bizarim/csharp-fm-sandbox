using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_ClearExplore : UserRedisQuery
    {
        public long i_biAccID = 0;
        public fmLordBase i_lordInfo = null;
        public List<rdItem> i_items = null;
        public List<rdMap> i_maps = null;
        public rdStat i_stat = null;

        public urq_ClearExplore(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 ||
               i_lordInfo == null || i_items == null || i_maps == null || i_stat == null)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            var trans = GetDatabase().CreateTransaction();

            trans.SetLordBase(i_biAccID, i_lordInfo);
            trans.SetLordItems(i_biAccID, i_items);
            trans.SetMaps(i_biAccID, i_maps);
            trans.SetLordStat(i_biAccID, i_stat);

            if (false == trans.Execute())
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_biAccID = 0;
            i_lordInfo = null;
            i_maps = null;
            i_stat = null;
        }
    }
}

using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_RefreshInDun : UserRedisQuery
    {
        public long i_biAccID = 0;
        public List<rdInDun>    i_induns = null;
        public fmLordBase       i_lordBase = null;

        public urq_RefreshInDun(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || null == i_induns || null == i_lordBase)
                return eErrorCode.Query_Params;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            var trans = GetDatabase().CreateTransaction();

            trans.SetLordBase(i_biAccID, i_lordBase);
            trans.SetLordInDuns(i_biAccID, i_induns);

            if (false == trans.Execute())
                return eErrorCode.Query_Fail;

            return eErrorCode.Success;
        }

        public override void Release()
        {
            i_induns = null;
            i_lordBase = null;
        }
    }
}

using appGameServer.Table;
using fmCommon;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    // user redis query

    public class urq_CreateLord : UserRedisQuery
    {
        public long i_biAccID = 0;
        public string i_strName = string.Empty;
        public fmLord o_lord = null;

        public urq_CreateLord(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            if (i_biAccID == 0 || true == string.IsNullOrEmpty(i_strName))
                return eErrorCode.Query_Params;

            if (true == IsExistsName(i_strName))
                return eErrorCode.Lord_AlreadyExistName;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            var trans = GetDatabase().CreateTransaction();

            fmLordBase lordInfo = null;
            rdStat lordStat = null;
            List<rdItem> items = null;
            List<rdMission> missions = null;
            List<rdMap> maps = null;
            fmMissionBase missionBase = null;
            List<rdInDun> inDuns = null;

            if (false == theLordCreater.TryCreate(i_biAccID, i_strName, out o_lord))
                return eErrorCode.Server_TableError;

            o_lord.TryGetItems(out items);
            o_lord.TryGetLordBase(out lordInfo);
            o_lord.TryGetStat(out lordStat);

            trans.InsertName(i_strName, i_biAccID);
            trans.SetLordBase(i_biAccID, lordInfo);
            trans.SetLordStat(i_biAccID, lordStat);
            trans.SetLordItems(i_biAccID, items);
            trans.SetMissions(i_biAccID, missions);
            trans.SetMaps(i_biAccID, maps);
            trans.SetMissionBase(i_biAccID, missionBase);
            trans.SetLordInDuns(i_biAccID, inDuns);

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

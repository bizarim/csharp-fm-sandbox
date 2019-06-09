using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        public eErrorCode UpToAncient(ref rdItem remeltItem)
        {
            rdOption keepOpt = null;

            foreach (var node in remeltItem.AddOpts)
            {
                if (eOptGrade.Legend == node.Grade || eOptGrade.Set == node.Grade)
                {
                    keepOpt = node;
                }
            }

            if (null == keepOpt)
                return eErrorCode.Error;

 
            int lv = remeltItem.Lv;
            eGrade grade = remeltItem.Grade;
            eParts parts = remeltItem.Parts;

            bool bChanged = false;
            int changeCode = 0;

            //if (parts == eParts.Jewel)
            //{
            //    bChanged = true;
            //    changeCode = remeltItem.Code;
            //}
            //else 
            if (parts == eParts.Weapon)
                bChanged = ChangeWeaponImageCode(eBeyond.Ancient, remeltItem.Code, out changeCode);
            else
                bChanged = ChangePartsImageCode(eBeyond.Ancient, parts, remeltItem.BaseOpt.ElementAt(0).Kind, out changeCode);

            if (false == bChanged)
            {
                Logger.Error("Failed. UpToAncient {0} / {1} / {2}", parts, remeltItem.Code, remeltItem.BaseOpt.ElementAt(0).Kind);
                return eErrorCode.Server_TableError;
            }

            remeltItem.Code = changeCode;
            remeltItem.AddOpts.Clear();

            List<eOption> temp = GetAncientOptList(parts);

            int cnt = GetOptionCount(grade);

            for (int i = 0; i < cnt; ++i)
            {
                int hit = m_random.Next(0, temp.Count);

                eOption kind = temp.ElementAt(hit);

                rdOption option = new rdOption(i + 1, false, GetOptGrade(kind), kind, GetAncientValue(lv, kind));
                remeltItem.AddOpts.Add(option);

                temp.RemoveAt(hit);
            }

            {
                remeltItem.AddOpts.Add(keepOpt);
            }

            remeltItem.Beyond = eBeyond.Ancient;

            return eErrorCode.Success;
        }
    }
}

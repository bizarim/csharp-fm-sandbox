using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        public eErrorCode Remelt(ref rdItem remeltItem, int optIndex, eOption selectedOpt)
        {
            if (null == remeltItem)
                return eErrorCode.Auth_PleaseLogin;

            eOption kind = selectedOpt;
            eOptGrade cc = (eOptGrade)((int)kind >> 8);
            eOption normKind = eOption.None;
            eOption epicKind = eOption.None;
            if (cc == eOptGrade.Normal)
            {
                normKind = kind;
                epicKind = (eOption)((int)kind + ((int)eOptGrade.Normal << 8));
            }
            else if (cc == eOptGrade.Epic)
            {
                normKind = (eOption)((int)kind - ((int)eOptGrade.Normal << 8));
                epicKind = kind;
            }

            rdOption remeltOpt = null;
            foreach (var node in remeltItem.AddOpts)
            {
                if (node.Index == optIndex)
                    remeltOpt = node;
                else
                {
                    if (true == node.Remelt)
                        return eErrorCode.Item_Remelt_AlreadyOther;
                    else
                    {
                        if (node.Kind == normKind)
                            return eErrorCode.Item_AlreadySameOpt;

                        if (node.Kind == epicKind)
                            return eErrorCode.Item_AlreadySameOpt;
                    }
                }

            }

            if (null == remeltOpt)
                return eErrorCode.Params_InvalidParam;

            remeltOpt.Remelt = true;

            Logger.Debug("optIndex {0}", optIndex);
            Logger.Debug("remeltOpt.Grade {0}", remeltOpt.Grade);


            if (eOptGrade.Legend == remeltOpt.Grade || eOptGrade.Set == remeltOpt.Grade)
                return eErrorCode.Item_OutOfType;


            remeltOpt.Kind = selectedOpt;
            remeltOpt.Grade = GetOptGrade(selectedOpt);

            if (eBeyond.None == remeltItem.Beyond)
                remeltOpt.Value = GetValue(remeltItem.Lv, remeltOpt.Kind);
            else
                remeltOpt.Value = GetAncientValue(remeltItem.Lv, remeltOpt.Kind);
            


            return eErrorCode.Success;
        }
    }
}

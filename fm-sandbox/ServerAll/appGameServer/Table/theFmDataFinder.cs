using fmCommon;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class theFmDataFinder
    {
        private static fmDataTable m_tableFmData = null;

        public static bool Load(fmDataTable table)
        {
            if (null == table)
                return false;

            m_tableFmData = table;

            return true;
        }

        public static T Find<T>(int code) where T : fmData
        {
            return m_tableFmData.Find<T>(code);
        }

        public static Dictionary<int, T> Find<T>(eFmDataType eType) where T : fmData
        {
            return m_tableFmData.Find<T>(eType);
        }
    }
}

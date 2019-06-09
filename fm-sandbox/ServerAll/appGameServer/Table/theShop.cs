using fmCommon;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class theShop
    {
        private static Dictionary<int, fmDataShop> m_shop = new Dictionary<int, fmDataShop>();

        public static bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataShop> dic = table.Find<fmDataShop>(eFmDataType.Shop);
            if (null == dic)
                return false;

            m_shop = dic;

            return true;
        }

        public static fmDataShop Find(int code)
        {
            if (false == m_shop.ContainsKey(code))
                return null;

            return m_shop[code];
        }
    }
}

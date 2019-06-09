using appGameServer.Table;
using Compress;
using fmCommon;
using fmCommon.Formula;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appGameServer
{
    /// <summary>
    /// 영주 아이템
    /// </summary>
    public partial class fmLord
    {
        protected ItemInventory m_itemInven = new ItemInventory();

        // 아이템
        public bool InitItems(List<rdItem> items) { return m_itemInven.Initialize(items); }

        //public bool InitPrevEnchantList(rdEnchantList preEnchantList) { return m_itemInven.SetPrevEnchantList(preEnchantList); }

        /// <summary>
        /// 인벤토리 정보 in server
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool TryGetItems(out List<rdItem> items) { return m_itemInven.TryGetItems(out items); }

        /// <summary>
        /// 인벤토리 정보 to clinet
        /// </summary>
        /// <param name="send"></param>
        /// <returns></returns>
        public bool TryGetItems(LZ4_PT_CG_Item_GetList_RS send)
        {
            if (true == m_itemInven.TryGetItems(out send.m_rdItems))
            {
                send.m_eErrorCode = eErrorCode.Success;
                return true;
            }
            else
            {
                send.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                return false;
            }
        }

        /// <summary>
        /// 아이템 장착
        /// </summary>
        /// <param name="recv"></param>
        /// <param name="send"></param>
        /// <returns></returns>
        public bool TryEquip(PT_CG_Item_Equip_RQ recv, PT_CG_Item_Equip_RS send)
        {
            send.m_eErrorCode = m_itemInven.Equip(recv.m_nSlot, GetLv());
            if (send.m_eErrorCode == eErrorCode.Success)
            {
                using (urq_EquipItem query = new urq_EquipItem(eRedis.Game))
                {
                    query.i_biAccID = AccId;
                    TryGetItems(out query.i_items);
                    send.m_eErrorCode = query.Execute();
                    //
                    // 성공 일때 클라인언트로 보내 줄거 파라미터 정하기
                }
            }

            return true;
        }

        public eErrorCode TryTakeItem(rdItem item)
        {
            return m_itemInven.Take(item);
        }
    }
}

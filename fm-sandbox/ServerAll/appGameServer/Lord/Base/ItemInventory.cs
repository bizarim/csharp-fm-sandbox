using appGameServer.Table;
using fmCommon;
using fmCommon.Formula;
using fmLibrary;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    public class ItemInventory : IDisposable
    {
        //private readonly object m_objLock = new object();

        protected Dictionary<eParts, int> m_dicEquip = new Dictionary<eParts, int>();
        protected Dictionary<int, bool> m_dicSlots = new Dictionary<int, bool>();
        protected List<rdItem> m_rdItems = null;


        public bool Initialize(List<rdItem> list)
        {
            m_dicEquip.Clear();
            m_dicSlots.Clear();

            int maxSlot = theGameConst.MaxInvenFullCnt;

            for (int i = 1; i <= maxSlot; ++i)
                m_dicSlots.Add(i, false);

            foreach (var node in list)
            {
                if (true == node.Equip)
                {
                    if (true == m_dicEquip.ContainsKey(node.Parts))
                    {
                        Logger.Error("ItemInventory Initialize : ContainsKey Parts, code {0}", node.Parts, node.Code);
                        return false;
                    }

                    m_dicEquip.Add(node.Parts, node.Slot);
                }

                UseSlot(node.Slot);

            }
            m_rdItems = list;

            return true;
        }

        public bool TryGetItems(out List<rdItem> items)
        {
            //items = null;
            items = m_rdItems;

            return true;
        }

        bool m_bNewEquip = false;
        public bool CheckNewEquip() { return m_bNewEquip; }

        public eErrorCode Equip(int selectSlot, int lordLv)
        {
            eErrorCode err = CheckSlot(selectSlot);
            if (eErrorCode.Success != err)
                return err;

            rdItem selectItem = m_rdItems.Find(x => x.Slot == selectSlot);
            if (null == selectItem)
            {
                Logger.Error("ItemInventory Equip: item == null Slot {0}", selectSlot);
                return eErrorCode.Auth_PleaseLogin;
            }

            if (lordLv < selectItem.Lv)
                return eErrorCode.Item_NotEnouhgLv;

            if (false == m_dicEquip.ContainsKey(selectItem.Parts))
                m_dicEquip.Add(selectItem.Parts, selectSlot);
            else
            {
                int oldSlot = m_dicEquip[selectItem.Parts];

                rdItem oldItem = m_rdItems.Find(x => x.Slot == oldSlot);
                if (null == oldItem)
                {
                    Logger.Error("ItemInventory Equip: oldItem == null Slot {0}", selectSlot);
                    return eErrorCode.Auth_PleaseLogin;
                }

                oldItem.Equip = false;
                m_dicEquip[selectItem.Parts] = selectSlot;
            }
            //return eErrorCode.Item_FailToEquip;

            selectItem.Equip = true;
            m_bNewEquip = true;

            return eErrorCode.Success;
        }

        public eErrorCode Sell(List<int> slots, out int gold)
        {
            gold = 0;

            //if (null == m_preEnchantList)
            //    return eErrorCode.Server_Error;

            foreach (var node in slots)
            {
                rdItem item = m_rdItems.Find(x => x.Slot == node);
                if (null == item)
                {
                    Logger.Error("1-ItemInventory Sell: Item == null Slot {0}", node);
                    return eErrorCode.Item_NoneExist;
                }

                if (true == item.Equip)
                    return eErrorCode.Item_DoNotSellEquiped;
            }

            foreach (var node in slots)
            {
                //if (m_preEnchantList.Exist)
                //{
                //    if (m_preEnchantList.ItemSlot == node)
                //        continue;
                //}

                rdItem item = m_rdItems.Find(x => x.Slot == node);
                if (null == item)
                {
                    Logger.Error("2-ItemInventory Sell: Item == null Slot {0}", node);
                    return eErrorCode.Auth_PleaseLogin;
                }

                if (false == m_rdItems.Remove(item))
                    return eErrorCode.Item_FailToSell;

                NoUseSlot(node);

                gold += (int)(item.Grade) * theFormula.SellPrice;
            }

            return eErrorCode.Success;
        }
        

        private eErrorCode Remove(rdItem item)
        {
            int slot = item.Slot;

            if (false == m_rdItems.Remove(item))
                return eErrorCode.Item_FailRemove;

            if (false == NoUseSlot(slot))
                return eErrorCode.Item_FailRemove;

            return eErrorCode.Success;
        }

        public eErrorCode Take(rdItem item)
        {
            if (null == item)
                return eErrorCode.Item_NoneExist;

            int newSlot = GetEmpty();
            if (0 == newSlot)
                return eErrorCode.Item_NotEnoughSlot;

            item.Slot = newSlot;
            UseSlot(newSlot);
            m_rdItems.Add(item);

            return eErrorCode.Success;
        }

        public bool TryFind(int slot, out rdItem item)
        {
            item = m_rdItems.Find(x => x.Slot == slot);
            if (null == item)
                return false;

            return true;
        }

        public eErrorCode CheckFull()
        {
            if (theGameConst.MaxHaveItemCnt <= m_rdItems.Count)
                return eErrorCode.Item_NotEnoughSlot;

            return eErrorCode.Success;
        }

        private int GetEmpty()
        {
            // linq 대신 수동으로....
            foreach (var node in m_dicSlots)
            {
                if (false == node.Value)
                {
                    return node.Key;
                }
            }

            return 0;
        }
        private eErrorCode CheckSlot(int slot)
        {
            if (false == m_dicSlots.ContainsKey(slot))
                return eErrorCode.Item_OutOfSlot;

            if (false == m_dicSlots[slot])
                return eErrorCode.Item_NoneExist;

            return eErrorCode.Success;
        }
        private bool UseSlot(int index)
        {
            if (false == m_dicSlots.ContainsKey(index))
                return false;

            m_dicSlots[index] = true;
            return true;
        }
        private bool NoUseSlot(int index)
        {
            if (false == m_dicSlots.ContainsKey(index))
                return false;

            m_dicSlots[index] = false;
            return true;
        }


        // ---------------------------------
        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ItemInventory()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (m_rdItems != null)
                {
                    foreach (var node in m_rdItems) { node.Dispose(); }
                    m_rdItems.Clear();
                    m_rdItems = null;
                }

                if (m_dicSlots != null)
                {
                    m_dicSlots.Clear();
                    m_dicSlots = null;
                }

                if (m_dicEquip != null)
                {
                    m_dicEquip.Clear();
                    m_dicEquip = null;
                }
            }
            m_disposed = true;
        }
    }
}

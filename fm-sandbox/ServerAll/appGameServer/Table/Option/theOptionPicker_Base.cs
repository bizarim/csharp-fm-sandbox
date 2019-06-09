using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public partial class theOptionPicker : Singleton<theOptionPicker>
    {
        private Random m_random = new Random();

        private Dictionary<int, fmDataItem> m_dataItems = null;

        private Dictionary<eParts, Dictionary<eOption, fmDataItem>> m_items = new Dictionary<eParts, Dictionary<eOption, fmDataItem>>();
        private Dictionary<eParts, Dictionary<eOption, fmDataItem>> m_ancientItems = new Dictionary<eParts, Dictionary<eOption, fmDataItem>>();
        private Dictionary<eParts, Dictionary<eOption, fmDataItem>> m_mythicItems = new Dictionary<eParts, Dictionary<eOption, fmDataItem>>();

        private Dictionary<eBeyond, Dictionary<eWeapon, fmDataItem>> m_weapons = new Dictionary<eBeyond, Dictionary<eWeapon, fmDataItem>>();

        private fmFormulaOption m_option = new fmFormulaOption();

        private fmOptListDrop m_dropOptList = new fmOptListDrop();
        private fmOptListEnchAnci m_enchAnciOptList = new fmOptListEnchAnci();
        private fmOptListMythic m_createMythicOptList = new fmOptListMythic();
        
        public bool Load(fmDataTable table)
        {
            if (false == LoadData(table)) return false;
            if (false == m_option.Load()) return false;
            
            if (false == m_enchAnciOptList.Load(table)) return false;
            if (false == m_dropOptList.Load(table)) return false;
            if (false == m_createMythicOptList.Load(table)) return false;

            return true;
        }

        private bool LoadDataDrop(Dictionary<int, fmDataItem> dic)
        {
            foreach (var node in dic)
            {
                if (node.Value.m_eBeyond == eBeyond.None)
                {
                    foreach (var op in node.Value.m_nArrOptions)
                    {
                        if (node.Value.m_eParts == eParts.Weapon)
                            continue;

                        if (false == m_items.ContainsKey(node.Value.m_eParts))
                            m_items.Add(node.Value.m_eParts, new Dictionary<eOption, fmDataItem>());

                        if (false == m_items[node.Value.m_eParts].ContainsKey((eOption)op))
                        {
                            m_items[node.Value.m_eParts].Add((eOption)op, node.Value);
                        }
                    }

                    if (node.Value.m_eWeapon != eWeapon.None)
                    {
                        if (false == m_weapons.ContainsKey(node.Value.m_eBeyond))
                            m_weapons.Add(node.Value.m_eBeyond, new Dictionary<eWeapon, fmDataItem>());

                        if (false == m_weapons[node.Value.m_eBeyond].ContainsKey(node.Value.m_eWeapon))
                        {
                            m_weapons[node.Value.m_eBeyond].Add(node.Value.m_eWeapon, node.Value);
                        }
                    }
                }
            }
            return true;
        }

        private bool LoadDataAncient(Dictionary<int, fmDataItem> dic)
        {
            foreach (var node in dic)
            {
                if (node.Value.m_eBeyond == eBeyond.Ancient)
                {
                    foreach (var op in node.Value.m_nArrOptions)
                    {
                        if (node.Value.m_eParts == eParts.Weapon)
                            continue;

                        if (false == m_ancientItems.ContainsKey(node.Value.m_eParts))
                            m_ancientItems.Add(node.Value.m_eParts, new Dictionary<eOption, fmDataItem>());

                        if (false == m_ancientItems[node.Value.m_eParts].ContainsKey((eOption)op))
                        {
                            m_ancientItems[node.Value.m_eParts].Add((eOption)op, node.Value);
                        }
                    }

                    if (node.Value.m_eWeapon != eWeapon.None)
                    {
                        if (false == m_weapons.ContainsKey(node.Value.m_eBeyond))
                            m_weapons.Add(node.Value.m_eBeyond, new Dictionary<eWeapon, fmDataItem>());

                        if (false == m_weapons[node.Value.m_eBeyond].ContainsKey(node.Value.m_eWeapon))
                        {
                            m_weapons[node.Value.m_eBeyond].Add(node.Value.m_eWeapon, node.Value);
                        }
                    }
                }
            }
            return true;
        }

        private bool LoadDataMythic(Dictionary<int, fmDataItem> dic)
        {
            foreach (var node in dic)
            {
                if (node.Value.m_eBeyond == eBeyond.Mythic)
                {
                    foreach (var op in node.Value.m_nArrOptions)
                    {
                        if (node.Value.m_eParts == eParts.Weapon)
                            continue;

                        if (false == m_mythicItems.ContainsKey(node.Value.m_eParts))
                            m_mythicItems.Add(node.Value.m_eParts, new Dictionary<eOption, fmDataItem>());

                        if (false == m_mythicItems[node.Value.m_eParts].ContainsKey((eOption)op))
                        {
                            m_mythicItems[node.Value.m_eParts].Add((eOption)op, node.Value);
                        }
                    }

                    if (node.Value.m_eWeapon != eWeapon.None)
                    {
                        if (false == m_weapons.ContainsKey(node.Value.m_eBeyond))
                            m_weapons.Add(node.Value.m_eBeyond, new Dictionary<eWeapon, fmDataItem>());

                        if (false == m_weapons[node.Value.m_eBeyond].ContainsKey(node.Value.m_eWeapon))
                        {
                            m_weapons[node.Value.m_eBeyond].Add(node.Value.m_eWeapon, node.Value);
                        }
                    }
                }
            }
            return true;
        }

        private bool LoadData(fmDataTable table)
        {
            Dictionary<int, fmDataItem> dic = table.Find<fmDataItem>(eFmDataType.Item);
            if (null == dic)
                return false;

            m_dataItems = dic;
            if (null == m_dataItems)
                return false;

            if (false == LoadDataDrop(dic)) return false;
            if (false == LoadDataAncient(dic)) return false;
            if (false == LoadDataMythic(dic)) return false;

            return true;
        }

        private int GetOptionCount(eGrade grade, eBeyond beyond = eBeyond.None)
        {
            if (beyond == eBeyond.Mythic)
            {
                return 5;
            }
            else
            {
                switch (grade)
                {
                    case eGrade.None: return 0;
                    case eGrade.Normal: return 1;
                    case eGrade.Magic: return 2;
                    case eGrade.Rare: return 3;
                    case eGrade.Epic: return 4;
                    case eGrade.Legend: return 4;
                    case eGrade.Set: return 4;
                    default:
                        return 0;
                }
            }

        }

        private eGrade GetGradeByAddOptCnt(int cnt, eBeyond beyond = eBeyond.None)
        {
            if (beyond == eBeyond.Mythic)
            {
                switch (cnt)
                {
                    case 0: return eGrade.None;
                    case 1: return eGrade.Normal;
                    case 2: return eGrade.Normal;
                    case 3: return eGrade.Magic;
                    case 4: return eGrade.Rare;
                    case 5: return eGrade.Epic;
                    default:
                        return eGrade.None;
                }
            }
            else
            {
                switch (cnt)
                {
                    case 0: return eGrade.None;
                    case 1: return eGrade.Normal;
                    case 2: return eGrade.Magic;
                    case 3: return eGrade.Rare;
                    case 4: return eGrade.Epic;
                    default:
                        return eGrade.None;
                }
            }
        }

        private eOptGrade GetOptGrade(eOption opt)
        {
            // eOptCategory
            eOptGrade cc = (eOptGrade)((int)opt >> 8);

            return cc;

            //if (cc == eOptGrade.Normal) return eGrade.Normal;
            //else if (cc == eOptGrade.Epic) return eGrade.Epic;
            //else if (cc == eOptGrade.Legend) return eGrade.Legend;
            //else if (cc == eOptGrade.Set) return eGrade.Set;
            //else return eGrade.Normal;
        }

        public eOptGrade GetLegendSetOptGrade(eGrade grade)
        {
            if (grade == eGrade.Legend) return eOptGrade.Legend;
            else if (grade == eGrade.Set) return eOptGrade.Set;
            else return eOptGrade.Normal;
        }

        private List<eOption> GetList(eGrade grade, eParts parts, int lv)
        {
            return m_dropOptList.GetList(grade, parts, lv);
        }

        private List<eOption> GetAncientOptList(eParts parts)
        {
            return m_enchAnciOptList.GetList(parts);
        }

        private eOption GetLegendSetOption(eGrade grade, eParts parts, int lv)
        {
            List<eOption> temp = GetList(grade, parts, lv);
            return temp.ElementAt(m_random.Next(0, temp.Count));
        }


        private float GetValue(int lv, eOption option)
        {
            return m_option.GetValue(lv, option);
        }

        private void GetBaseValue(int lv, eOption option, out float value)
        {
            // 기본 베이스 옵션 값 선택
            value = m_option.GetBaseValue(lv, option);
        }

        private void GetBaseValue(int lv, eParts parts, out eOption option, out float value, out int code)
        {
            //Logger.Debug("GetBaseValue: {0}" , parts);
            int cnt = m_items[parts].Count;
            int hit = m_random.Next(0, cnt);
            var data = m_items[parts].ElementAt(hit).Value;
            code = data.m_nCode;
            option = m_items[parts].ElementAt(hit).Key;
            GetBaseValue(lv, option, out value);
        }


        private float GetAncientValue(int lv, eOption option)
        {
            return m_option.GetAncientValue(lv, option);
        }


        private void GetBaseOpt(rdItem item)
        {
            eOptGrade grade = eOptGrade.Normal;
            // 무기
            if (item.Parts == eParts.Weapon)
            {
                //item.Code = GetWeaponCode();

                float min = 0;
                float max = 0;
                float speed = 0;
                item.Code = GetWeapon(item.Lv, out min, out max, out speed);

                // 1. 무기 최소 공격력
                {
                    eOption select = eOption.BWDMin;
                    //float value = 0;
                    //GetBaseValue(item.Lv, select, out value);
                    rdOption option = new rdOption(1, false, grade, select, min);
                    item.BaseOpt.Add(option);
                }
                // 2. 무기 최대 공격력
                {
                    eOption select = eOption.BWDMax;
                    //float value = 0;
                    //GetBaseValue(item.Lv, select, out value);
                    rdOption option = new rdOption(2, false, grade, select, max);
                    item.BaseOpt.Add(option);
                }
                // 4. 공속
                {
                    eOption select = eOption.AS;
                    //float value = 0;
                    //GetBaseValue(item.Lv, select, out value);
                    rdOption option = new rdOption(3, false, grade, select, speed);
                    item.BaseOpt.Add(option);
                }
                // 3. 속성
                {
                    eOption select = eOption.Element;
                    float value = 0f;
                    {
                        int b = (int)eElement.None;
                        int e = 1 + (int)eElement.Nature;
                        value = m_random.Next(b, e);
                    }
                    rdOption option = new rdOption(4, false, grade, select, value);
                    item.BaseOpt.Add(option);
                }
            }
            else
            {
                // 기타
                {
                    eOption select = eOption.None;
                    float value = 0;
                    int code = 0;
                    GetBaseValue(item.Lv, item.Parts, out select, out value, out code);
                    rdOption option = new rdOption(1, false, grade, select, value);
                    item.Code = code;
                    item.BaseOpt.Add(option);
                }
            }

        }

        private int GetWeapon(int lv, out float min, out float max, out float speed)
        {
            var data = GetWeaponData();
            eWeapon type = data.m_eWeapon;

            m_option.GetWeapon(type, lv, out min, out max, out speed);

            return data.Code;
        }


        private fmDataItem GetWeaponData()
        {
            int cnt = m_weapons[eBeyond.None].Count;

            int hit = m_random.Next(0, cnt);

            var data = m_weapons[eBeyond.None].ElementAt(hit).Value;

            return data;
        }

        private bool ChangeWeaponImageCode(eBeyond beyond, int oriCode, out int outCode)
        {
            outCode = 0;

            if (true == m_dataItems.ContainsKey(oriCode))
            {
                eWeapon type = m_dataItems[oriCode].m_eWeapon;

                if (true == m_weapons.ContainsKey(beyond))
                {
                    if (true == m_weapons[beyond].ContainsKey(type))
                    {
                        outCode = m_weapons[beyond][type].Code;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ChangePartsImageCode(eBeyond beyond, eParts parts, eOption opt, out int code)
        {
            code = 0;
            if (beyond == eBeyond.Ancient)
            {
                if (true == m_ancientItems.ContainsKey(parts))
                {
                    if (true == m_ancientItems[parts].ContainsKey(opt))
                    {
                        code = m_ancientItems[parts][opt].Code;
                        return true;
                    }
                }

            }
            else if (beyond == eBeyond.Mythic)
            {

                if (true == m_mythicItems.ContainsKey(parts))
                {
                    if (true == m_mythicItems[parts].ContainsKey(opt))
                    {
                        code = m_mythicItems[parts][opt].Code;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

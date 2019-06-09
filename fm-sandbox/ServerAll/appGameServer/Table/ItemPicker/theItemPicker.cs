using appGameServer.Table;
using fmCommon;
using fmServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace appGameServer.Table
{
    public class theItemPicker : Singleton<theItemPicker>
    {
        //private static Random m_random = new Random();
        private List<fmDataDropValue> m_valuesInDTomb = null;
        private List<fmDataDropValue> m_valuesInWorld = null;

        private  List<fmGochaItem> m_gochaAll = new List<fmGochaItem>();
        private  List<fmGochaItem> m_gochaWitoutJewel = new List<fmGochaItem>();

        private  List<eParts> m_parts = new List<eParts>();

        public bool Load(fmDataTable table)
        {
            Dictionary<int, fmDataDropValue> dic = table.Find<fmDataDropValue>(eFmDataType.DropValue);
            if (null == dic)
                return false;

            if (false == LoadDropValueInDTomb(dic))
                return false;

            if (false == LoadDropValueInWorld(dic))
                return false;

            if (false == LoadPartsList())
                return false;

            return true;
        }
        
        private bool LoadPartsList()
        {
            m_parts.Clear();
            m_parts.Add(eParts.Weapon  );
            m_parts.Add(eParts.Necklace);
            m_parts.Add(eParts.Ring    );
            m_parts.Add(eParts.Belt    );
            m_parts.Add(eParts.Gloves  );
            m_parts.Add(eParts.Pants   );
            m_parts.Add(eParts.Armor   );
            m_parts.Add(eParts.Head    );
            //m_parts.Add(eParts.Jewel   );

            return true;
        }

        private bool LoadDropValueInDTomb(Dictionary<int, fmDataDropValue> dic)
        {
            int m_nMaxVal = 0;
            int cnt = dic.Count;
            for (int i = cnt - 1; 0 <= i; --i)
            {
                var data = dic.ElementAt(i).Value;
                int roll = data.m_nRate;

                fmGochaItem node = new fmGochaItem();
                node.m_fmData = data;
                node.m_nGochaValue = 0;
                node.m_nBegin = m_nMaxVal;
                node.m_nEnd = m_nMaxVal + roll;
                m_gochaAll.Add(node);

                m_nMaxVal += roll;
            }

            m_valuesInDTomb = dic.Values.Where(x => x.m_strDropPlace.Equals("DTomb")).ToList();

            return true;
        }

        private bool LoadDropValueInWorld(Dictionary<int, fmDataDropValue> dic)
        {
            int m_nMaxVal = 0;
            int cnt = dic.Count;
            for (int i = cnt - 1; 0 <= i; --i)
            {
                var data = dic.ElementAt(i).Value;
                int roll = data.m_nRate;

                if (data.m_eParts == eParts.Jewel)
                    continue;

                fmGochaItem node = new fmGochaItem();
                node.m_fmData = data;
                node.m_nGochaValue = 0;
                node.m_nBegin = m_nMaxVal;
                node.m_nEnd = m_nMaxVal + roll;
                m_gochaWitoutJewel.Add(node);

                m_nMaxVal += roll;
            }

            m_valuesInWorld = dic.Values.Where(x => x.m_strDropPlace.Equals("World")).ToList();

            return true;
        }

        public void DropInWorld(Random lordRandom, int lv, int dropLimitValue, out fmDropItem item)
        {
            //int itemLv = m_random.Next((lv - 1) ==, (lv + 1));

            item = new fmDropItem
            {
                Lv = lv,
                Grade = eGrade.Normal,
                Parts = eParts.Gloves
            };

            List<fmDataDropValue> table = new List<fmDataDropValue>();
            table.Clear();

            int max = 0;

            foreach (var node in m_valuesInWorld)
            {
                if (node.m_nLimitValue < dropLimitValue) continue;
                max += node.m_nRate;
                table.Add(node);
            }

            int hit = lordRandom.Next(0, max);

            foreach (var node in m_gochaWitoutJewel)
            {
                if (node.m_nBegin <= hit && hit < node.m_nEnd)
                {
                    item.Grade = node.m_fmData.m_eGrade;
                    item.Parts = node.m_fmData.m_eParts;
                    return;
                }
            }

        }

        public void DropInDTomb(Random lordRandom, int lv, int dropLimitValue, out fmDropItem item)
        {

            item = new fmDropItem
            {
                Lv = lv,
                Grade = eGrade.Normal,
                Parts = eParts.Gloves
            };

            List<fmDataDropValue> table = new List<fmDataDropValue>();
            table.Clear();

            int max = 0;

            foreach (var node in m_valuesInDTomb)
            {
                if (node.m_nLimitValue < dropLimitValue) continue;
                max += node.m_nRate;
                table.Add(node);
            }

            int hit = lordRandom.Next(0, max);

            foreach (var node in m_gochaAll)
            {
                if (node.m_nBegin <= hit && hit < node.m_nEnd)
                {
                    item.Grade = node.m_fmData.m_eGrade;
                    item.Parts = node.m_fmData.m_eParts;
                    return;
                }
            }
        }

        public void DropInDunWithBoss(Random lordRandom, int lv, out fmDropItem item)
        {
            int hit = lordRandom.Next(0, m_parts.Count);

            item = new fmDropItem
            {
                Lv = lv,
                Grade = eGrade.Set,
                Parts = m_parts.ElementAt(hit)
            };

            //Console.WriteLine("DropInDunWithBoss");
            //Console.WriteLine(item.Grade);
            //Console.WriteLine(item.Parts);
        }

        public void BuyItem(Random lordRandom, eGrade grade, int lv, out fmDropItem item)
        {
            int cnt = m_parts.Count;
            int hit = lordRandom.Next(0, cnt);
            eParts parts = m_parts.ElementAt(hit);

            item = new fmDropItem
            {
                Lv = lv,
                Grade = grade,
                Parts = parts
            };
        }

        public void GetItemOnCombineCube(eParts parts, int lv, out fmDropItem item)
        {
            Random lordRandom = new Random();

            item = null;
            eGrade grade = eGrade.None;
            eParts selectParts = eParts.None;

            {
                int hit = lordRandom.Next(0, 100);
                if (hit < 49)
                    selectParts = parts;
            }

            if (selectParts == eParts.None)
            {
                int cnt = m_parts.Count;
                int hit = lordRandom.Next(0, cnt);
                selectParts = m_parts.ElementAt(hit);
            }

            {
                int hit = lordRandom.Next(0, 100);
                if (hit <= 70)
                    grade = eGrade.Legend;
                else
                    grade = eGrade.Set;
            }

            item = new fmDropItem
            {
                Lv = lv,
                Grade = grade,
                Parts = selectParts
            };


        }






        //public void GetItemWithSeal(eParts parts, int lv, out fmDropItem item)
        //{
        //    Random lordRandom = new Random();

        //    item = null;
        //    eGrade grade = eGrade.None;
        //    eParts selectParts = parts;

        //    if (parts == eParts.None)
        //    {
        //        int cnt = m_parts.Count;
        //        int hit = lordRandom.Next(0, cnt);
        //        selectParts = m_parts.ElementAt(hit);
        //    }

        //    {
        //        int hit = lordRandom.Next(0, 10000);
        //        if (hit < 6000)
        //            grade = eGrade.Epic;
        //        else if(6000<= hit && hit < 8000)
        //            grade = eGrade.Legend;
        //        else
        //            grade = eGrade.Set;
        //    }

        //    item = new fmDropItem
        //    {
        //        Lv = lv,
        //        Grade = grade,
        //        Parts = selectParts
        //    };


        //}
    }
}

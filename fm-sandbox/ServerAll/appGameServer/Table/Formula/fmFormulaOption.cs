using fmCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    class fmFormulaOption
    {
        delegate float fnFormula(int lv);

        private Random m_random = new Random();

        Dictionary<eOption, fnFormula> m_values = new Dictionary<eOption, fnFormula>();

        Dictionary<eOption, fnFormula> m_valuesAncient = new Dictionary<eOption, fnFormula>();

        private void LoadNormla()
        {
            m_values.Clear();
            //m_values.Add(eOption.Element,               OnElement);
            m_values.Add(eOption.CriRate,               OnCriRate);
            m_values.Add(eOption.CriDamageRate,         OnCriDamageRate);
            m_values.Add(eOption.ResistAll,             OnResistAll);
            m_values.Add(eOption.ResistFire,            OnResistFire);
            m_values.Add(eOption.ResistIce,             OnResistIce);
            m_values.Add(eOption.ResistNature,          OnResistNature);
            m_values.Add(eOption.ResistNone,            OnResistNone);
            m_values.Add(eOption.EDFire,                OnEDFire);
            m_values.Add(eOption.EDIce,                 OnEDIce);
            m_values.Add(eOption.EDNature,              OnEDNature);
            m_values.Add(eOption.EDNone,                OnEDNone);
            m_values.Add(eOption.EDRateFire,            OnEDRateFire);
            m_values.Add(eOption.EDRateIce,             OnEDRateIce);
            m_values.Add(eOption.EDRateNature,          OnEDRateNature);
            m_values.Add(eOption.EDRateNone,            OnEDRateNone);
            m_values.Add(eOption.DEF,                   OnDEF);
            m_values.Add(eOption.HP,                    OnHP);
            m_values.Add(eOption.BWDMin,                OnBWDMin);
            m_values.Add(eOption.BWDMax,                OnBWDMax);
            m_values.Add(eOption.WD,                    OnWD);
            m_values.Add(eOption.WDRate,                OnWDRate);
            m_values.Add(eOption.AS,                    OnAS);
            m_values.Add(eOption.ASRate,                OnASRate);
            m_values.Add(eOption.ItemDropRate,          OnItemDropRate);
            m_values.Add(eOption.GoldDropRate,          OnGoldDropRate);
            m_values.Add(eOption.Recovery,              OnRecovery);
            m_values.Add(eOption.FindMagicItemRate,     OnFindMagicItemRate);
            m_values.Add(eOption.DEFRate,               OnDEFRate);
            m_values.Add(eOption.HPRate,                OnHPRate);
            m_values.Add(eOption.RecoveryRate,          OnRecoveryRate);

            m_values.Add(eOption.EpCriRate,             OnEpCriRate);
            m_values.Add(eOption.EpCriDamageRate,       OnEpCriDamageRate);
            m_values.Add(eOption.EpResistAll,           OnEpResistAll);
            m_values.Add(eOption.EpResistFire,          OnEpResistFire);
            m_values.Add(eOption.EpResistIce,           OnEpResistIce);
            m_values.Add(eOption.EpResistNature,        OnEpResistNature);
            m_values.Add(eOption.EpResistNone,          OnEpResistNone);
            m_values.Add(eOption.EpEDFire,              OnEpEDFire);
            m_values.Add(eOption.EpEDIce,               OnEpEDIce);
            m_values.Add(eOption.EpEDNature,            OnEpEDNature);
            m_values.Add(eOption.EpEDNone,              OnEpEDNone);
            m_values.Add(eOption.EpEDRateFire,          OnEpEDRateFire);
            m_values.Add(eOption.EpEDRateIce,           OnEpEDRateIce);
            m_values.Add(eOption.EpEDRateNature,        OnEpEDRateNature);
            m_values.Add(eOption.EpEDRateNone,          OnEpEDRateNone);
            m_values.Add(eOption.EpDEF,                 OnEpDEF);
            m_values.Add(eOption.EpHP,                  OnEpHP);
            m_values.Add(eOption.EpBWDMin,              OnEpBWDMin);
            m_values.Add(eOption.EpBWDMax,              OnEpBWDMax);
            m_values.Add(eOption.EpWD,                  OnEpWD);
            m_values.Add(eOption.EpWDRate,              OnEpWDRate);
            m_values.Add(eOption.EpAS,                  OnEpAS);
            m_values.Add(eOption.EpASRate,              OnEpASRate);
            m_values.Add(eOption.EpItemDropRate,        OnEpItemDropRate);
            m_values.Add(eOption.EpGoldDropRate,        OnEpGoldDropRate);
            m_values.Add(eOption.EpRecovery,            OnEpRecovery);
            m_values.Add(eOption.EpFindMagicItemRate,   OnEpFindMagicItemRate);
            m_values.Add(eOption.EpDEFRate,             OnEpDEFRate);
            m_values.Add(eOption.EpHPRate,              OnEpHPRate);
            m_values.Add(eOption.EpRecoveryRate,        OnEpRecoveryRate);

            m_values.Add(eOption.ExtraAtkChance,        OnExtraAtkChance);
            m_values.Add(eOption.CrushingBlow,          OnCrushingBlow);
            m_values.Add(eOption.PlusSetEffect,         OnPlusSetEffect);
            m_values.Add(eOption.ExtraDMGToRareMon,     OnExtraDMGToRareMon);
            m_values.Add(eOption.LegnendThornRate,      OnLegnendThornRate  );
            m_values.Add(eOption.LegnendPoisonRate,     OnLegnendPoisonRate );
            m_values.Add(eOption.LegnendBurnRate  ,     OnLegnendBurnRate   );
            m_values.Add(eOption.LegnendFreezeRate,     OnLegnendFreezeRate);
            m_values.Add(eOption.LegnendDMGReduceRate,  OnLegnendDMGReduceRate);


            m_values.Add(eOption.SETAttack,             OnSETAttack);
            m_values.Add(eOption.SETLuck,               OnSETLuck);
            m_values.Add(eOption.SETFindMagicItemRate,  OnSETFindMagicItemRate  );
            m_values.Add(eOption.SETExpRate,            OnSETExpRate            );
            m_values.Add(eOption.SETFire,               OnSETFire               );
            m_values.Add(eOption.SETIce,                OnSETIce                );
            m_values.Add(eOption.SETNature,             OnSETNature             );
            m_values.Add(eOption.SETNone,               OnSETNone               );
            m_values.Add(eOption.SETThorn,              OnSETThorn              );
            m_values.Add(eOption.SETPoison,             OnSETPoison             );
            m_values.Add(eOption.SETExtraStone,         OnSETExtraStone         );
            m_values.Add(eOption.SETRecovery,           OnSETRecovery         );
            m_values.Add(eOption.SETHP      ,           OnSETHP               );
            m_values.Add(eOption.SETBurn    ,           OnSETBurn               );
            m_values.Add(eOption.SETFreeze  ,           OnSETFreeze             );

            m_values.Add(eOption.SETFireT3  ,   OnSETFireT3);
            m_values.Add(eOption.SETIceT3   ,   OnSETIceT3);
            m_values.Add(eOption.SETNatureT3,   OnSETNatureT3);
            m_values.Add(eOption.SETNoneT3,     OnSETNoneT3);


            //m_values.Add(eOption.AcDMGToRareMon,        OnAcDMGToRareMon    );
            //m_values.Add(eOption.AcAllResistRate,       OnAcAllResistRate   );
            //m_values.Add(eOption.AcHPRate,              OnAcHPRate          );
            ////m_values.Add(eOption.AcDEFRate,             OnAcDEFRate         );
            //m_values.Add(eOption.AcRecoveryRate,        OnAcRecoveryRate    );


        }

        private void LoadAncient()
        {
            m_valuesAncient.Clear();
            m_valuesAncient.Add(eOption.CriRate,               OnAncientCriRate);
            m_valuesAncient.Add(eOption.CriDamageRate,         OnAncientCriDamageRate);
            m_valuesAncient.Add(eOption.ResistAll,             OnAncientResistAll);
            m_valuesAncient.Add(eOption.ResistFire,            OnAncientResistFire);
            m_valuesAncient.Add(eOption.ResistIce,             OnAncientResistIce);
            m_valuesAncient.Add(eOption.ResistNature,          OnAncientResistNature);
            m_valuesAncient.Add(eOption.ResistNone,            OnAncientResistNone);
            m_valuesAncient.Add(eOption.EDFire,                OnAncientEDFire);
            m_valuesAncient.Add(eOption.EDIce,                 OnAncientEDIce);
            m_valuesAncient.Add(eOption.EDNature,              OnAncientEDNature);
            m_valuesAncient.Add(eOption.EDNone,                OnAncientEDNone);
            m_valuesAncient.Add(eOption.EDRateFire,            OnAncientEDRateFire);
            m_valuesAncient.Add(eOption.EDRateIce,             OnAncientEDRateIce);
            m_valuesAncient.Add(eOption.EDRateNature,          OnAncientEDRateNature);
            m_valuesAncient.Add(eOption.EDRateNone,            OnAncientEDRateNone);
            m_valuesAncient.Add(eOption.DEF,                   OnAncientDEF);
            m_valuesAncient.Add(eOption.HP,                    OnAncientHP);
            m_valuesAncient.Add(eOption.BWDMin,                OnAncientBWDMin);
            m_valuesAncient.Add(eOption.BWDMax,                OnAncientBWDMax);
            m_valuesAncient.Add(eOption.WD,                    OnAncientWD);
            m_valuesAncient.Add(eOption.WDRate,                OnAncientWDRate);
            m_valuesAncient.Add(eOption.AS,                    OnAncientAS);
            m_valuesAncient.Add(eOption.ASRate,                OnAncientASRate);
            m_valuesAncient.Add(eOption.ItemDropRate,          OnAncientItemDropRate);
            m_valuesAncient.Add(eOption.GoldDropRate,          OnAncientGoldDropRate);
            m_valuesAncient.Add(eOption.Recovery,              OnAncientRecovery);
            m_valuesAncient.Add(eOption.FindMagicItemRate,     OnAncientFindMagicItemRate);
            m_valuesAncient.Add(eOption.DEFRate,               OnAncientDEFRate);
            m_valuesAncient.Add(eOption.HPRate,                OnAncientHPRate);
            m_valuesAncient.Add(eOption.RecoveryRate,          OnAncientRecoveryRate);

            m_valuesAncient.Add(eOption.EpCriRate,             OnAncientEpCriRate);
            m_valuesAncient.Add(eOption.EpCriDamageRate,       OnAncientEpCriDamageRate);
            m_valuesAncient.Add(eOption.EpResistAll,           OnAncientEpResistAll);
            m_valuesAncient.Add(eOption.EpResistFire,          OnAncientEpResistFire);
            m_valuesAncient.Add(eOption.EpResistIce,           OnAncientEpResistIce);
            m_valuesAncient.Add(eOption.EpResistNature,        OnAncientEpResistNature);
            m_valuesAncient.Add(eOption.EpResistNone,          OnAncientEpResistNone);
            m_valuesAncient.Add(eOption.EpEDFire,              OnAncientEpEDFire);
            m_valuesAncient.Add(eOption.EpEDIce,               OnAncientEpEDIce);
            m_valuesAncient.Add(eOption.EpEDNature,            OnAncientEpEDNature);
            m_valuesAncient.Add(eOption.EpEDNone,              OnAncientEpEDNone);
            m_valuesAncient.Add(eOption.EpEDRateFire,          OnAncientEpEDRateFire);
            m_valuesAncient.Add(eOption.EpEDRateIce,           OnAncientEpEDRateIce);
            m_valuesAncient.Add(eOption.EpEDRateNature,        OnAncientEpEDRateNature);
            m_valuesAncient.Add(eOption.EpEDRateNone,          OnAncientEpEDRateNone);
            m_valuesAncient.Add(eOption.EpDEF,                 OnAncientEpDEF);
            m_valuesAncient.Add(eOption.EpHP,                  OnAncientEpHP);
            m_valuesAncient.Add(eOption.EpBWDMin,              OnAncientEpBWDMin);
            m_valuesAncient.Add(eOption.EpBWDMax,              OnAncientEpBWDMax);
            m_valuesAncient.Add(eOption.EpWD,                  OnAncientEpWD);
            m_valuesAncient.Add(eOption.EpWDRate,              OnAncientEpWDRate);
            m_valuesAncient.Add(eOption.EpAS,                  OnAncientEpAS);
            m_valuesAncient.Add(eOption.EpASRate,              OnAncientEpASRate);
            m_valuesAncient.Add(eOption.EpItemDropRate,        OnAncientEpItemDropRate);
            m_valuesAncient.Add(eOption.EpGoldDropRate,        OnAncientEpGoldDropRate);
            m_valuesAncient.Add(eOption.EpRecovery,            OnAncientEpRecovery);
            m_valuesAncient.Add(eOption.EpFindMagicItemRate,   OnAncientEpFindMagicItemRate);
            m_valuesAncient.Add(eOption.EpDEFRate,             OnAncientEpDEFRate);
            m_valuesAncient.Add(eOption.EpHPRate,              OnAncientEpHPRate);
            m_valuesAncient.Add(eOption.EpRecoveryRate,        OnAncientEpRecoveryRate);

            m_valuesAncient.Add(eOption.AcDMGToRareMonRate,     OnAcDMGToRareMon    );
            m_valuesAncient.Add(eOption.AcAllResistRate,       OnAcAllResistRate   );
            m_valuesAncient.Add(eOption.AcHPRate,              OnAcHPRate          );
            //m_valuesAncient.Add(eOption.AcDEFRate,             OnAcDEFRate         );
            m_valuesAncient.Add(eOption.AcRecoveryRate,        OnAcRecoveryRate    );
            
        }

        public bool Load()
        {
            LoadNormla();
            LoadAncient();

            return true;
        }

        public float GetValue(int lv, eOption option)
        {
            return m_values[option](lv);
        }

        public float GetBaseValue(int lv, eOption option)
        {
            float value = 0f;
            //if (option == eOption.BWDMin)
            //{
            //    value = GetBaseBWDMin(lv);
            //}
            //else if (option == eOption.BWDMax)
            //{
            //    value = GetBaseBWDMax(lv);
            //}
            //else if (option == eOption.AS)
            //{
            //    value = GetBaseAs();
            //}
            //else 
            if (option == eOption.HP)
            {
                value = GetValue(lv, option);
                value = (float)Math.Round(value * 3);
            }
            else if (option == eOption.DEF)
            {
                value = GetValue(lv, option);
                value = (float)Math.Round(value * 3);
            }
            else if (option == eOption.Element)
            {
                value = GetValue(lv, option);
            }
            else if (option == eOption.CriRate)
            {
                value = GetValue(lv, option);
                value = (float)Math.Round(value * 2);
            }
            else
            {
                value = GetValue(lv, option);
                value = (float)Math.Round(value * 4);
            }

            return value;
        }

        public void GetWeapon(eWeapon type, int lv, out float min, out float max, out float speed)
        {
            min = GetBaseBWDMin(type, lv);
            max = GetBaseBWDMax(type, lv);
            speed = GetBaseAs(type);
        }

        private int GetBaseBWDMin(eWeapon type, int lv)
        {
            int mb = 12;
            int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));
            int minBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));
            int hit = 0;

            if (type == eWeapon.Speed)
            {
                hit = m_random.Next(minBWD, minBWD + atkRate * 1);
            }
            else if (type == eWeapon.Sharp)
            {
                hit = m_random.Next(minBWD + atkRate * 1, minBWD + atkRate * 2);
            }
            else if (type == eWeapon.Steel)
            {
                hit = m_random.Next(minBWD + atkRate * 2, minBWD + atkRate * 3);
            }

            return hit;
        }
        private int GetBaseBWDMax(eWeapon type, int lv)
        {
            int mb = 18;
            int maxBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int hit = 0;

            if (type == eWeapon.Speed)
            {
                hit = m_random.Next(maxBWD - atkRate, maxBWD);
            }
            else if (type == eWeapon.Sharp)
            {
                hit = m_random.Next(maxBWD - atkRate, maxBWD + atkRate * 1);
            }
            else if (type == eWeapon.Steel)
            {
                hit = m_random.Next(maxBWD, maxBWD + atkRate * 1);
            }

            return hit;
        }

        //private int GetBaseBWDMin(int lv)
        //{
        //    int mb = 12;
        //    int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

        //    int minBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

        //    int hit = m_random.Next(minBWD, minBWD + atkRate * 3);
        //    return hit;
        //}
        //private int GetBaseBWDMax(int lv)
        //{
        //    int mb = 18;
        //    int maxBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

        //    int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

        //    int hit = m_random.Next(maxBWD - atkRate, maxBWD);
        //    return hit;
        //}

        private float GetBaseAs(eWeapon type)
        {
            float value = 1.00f;

            if (type == eWeapon.Speed)
            {
                int hit = m_random.Next(7, 10);
                value += (hit * 0.01f);
            }
            else if (type == eWeapon.Sharp)
            {
                int hit = m_random.Next(4, 7);
                value += (hit * 0.01f);
            }
            else if (type == eWeapon.Steel)
            {
                int hit = m_random.Next(1, 4);
                value += (hit * 0.01f);
            }

            return value;
        }

        //private float OnElement(int lv)
        //{
        //    int min = (int)eElement.None;
        //    int max = 1 + (int)eElement.Nature;

        //    return m_random.Next(min, max);
        //}
        private float OnCriRate(int lv)
        {
            // 레벨에 상관 없이
            // 0.5~6.0
            int hit = m_random.Next(15, 50);
            return (float)Math.Round(hit * 0.1f, 2);
        }
        private float OnCriDamageRate(int lv)
        {
            // 레벨에 상관 없이
            // 5.0~30.0
            //int hit = m_random.Next(50, 150);
            int hit = m_random.Next(80, 200);
            return (float)Math.Round(hit * 0.1f, 2);
        }
        private float OnResistAll(int lv)
        {
            //int hit = m_random.Next(1, 3);
            //int hit = m_random.Next(200, 500);
            int hit = m_random.Next(200, 700);
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private float OnResistFire(int lv)
        {
            //int hit = m_random.Next(2, 5);
            //int hit = m_random.Next(200, 500);
            int hit = m_random.Next(200, 700);
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private float OnResistIce(int lv)
        {
            //int hit = m_random.Next(2, 5);
            //int hit = m_random.Next(200, 500);
            int hit = m_random.Next(200, 700);
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private float OnResistNature(int lv)
        {
            //int hit = m_random.Next(2, 5);
            //int hit = m_random.Next(200, 500);
            int hit = m_random.Next(200, 700);
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private float OnResistNone(int lv)
        {
            //int hit = m_random.Next(2, 5);
            //int hit = m_random.Next(300, 800);
            int hit = m_random.Next(200, 700);
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private float OnEDFire(int lv)
        {
            // 5.0~30.0
            //int hit = m_random.Next(6, 11);
            //int hit = m_random.Next(12, 22);
            //int hit = m_random.Next(10, 20);
            //return (int)Math.Round((lv * hit * 0.1f) + 10f);

            //int hit = m_random.Next(20, 50);
            int hit = m_random.Next(30, 60);
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private float OnEDIce(int lv)
        {
            // 5.0~30.0
            //int hit = m_random.Next(6, 11);
            //int hit = m_random.Next(12, 22);
            //int hit = m_random.Next(10, 20);
            //return (int)Math.Round((lv * hit * 0.1f) + 10f);

            //int hit = m_random.Next(20, 50);
            int hit = m_random.Next(30, 60);
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private float OnEDNature(int lv)
        {
            // 5.0~30.0
            //int hit = m_random.Next(6, 11);
            //int hit = m_random.Next(12, 22);
            //int hit = m_random.Next(10, 20);
            //return (int)Math.Round((lv * hit * 0.1f) + 10f);

            //int hit = m_random.Next(20, 50);
            int hit = m_random.Next(30, 60);
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private float OnEDNone(int lv)
        {
            // 5.0~30.0
            //int hit = m_random.Next(6, 11);
            //int hit = m_random.Next(12, 22);
            //int hit = m_random.Next(10, 20);
            //return (int)Math.Round((lv * hit * 0.1f) + 10f);

            //int hit = m_random.Next(20, 50);
            int hit = m_random.Next(30, 60);
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private float OnEDRateFire(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnEDRateIce(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnEDRateNature(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnEDRateNone(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnDEF(int lv)
        {
            // 10.0~30.0
            //int hit = m_random.Next(100, 169);
            //int hit = m_random.Next(110, 250);
            //int hit = m_random.Next(110, 350);
            int hit = m_random.Next(110, 400);
            float get = (float)Math.Round(hit * 0.01f * (lv + 1));
            return get;
        }
        private float OnHP(int lv)
        {
            //int hit = m_random.Next(3000, 7500);
            //int hit = m_random.Next(8500, 11000);
            //int hit = m_random.Next(8500, 13000);
            //int hit = m_random.Next(8500, 14500);
            int hit = m_random.Next(9500, 16000);
            float get = (float)Math.Round(hit * 0.001 * (1.5 + 0.054 * (lv + 70) + lv * (lv - 1) * 0.01));
            return get;
        }
        private float OnBWDMin(int lv)
        {
            int temp = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002) / 4);


            int hit = m_random.Next(1, temp);
            return hit;
        }
        private float OnBWDMax(int lv)
        {
            int temp = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int hit = m_random.Next(1, temp);
            return hit;
        }
        private float OnWD(int lv)
        {
            // 5.0~30.0
            //int hit = m_random.Next(20, 50);
            //int hit = m_random.Next(18, 30);
            int hit = m_random.Next(12, 40);
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private float OnWDRate(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnAS(int lv)
        {
            // 레벨에 상관 없이
            int hit = m_random.Next(10, 120);
            return hit * 0.0001f;
        }
        private float OnASRate(int lv)
        {
            // 1.25~3.2
            // 레벨에 상관 없이
            int hit = m_random.Next(320, 500);
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private float OnItemDropRate(int lv)
        {
            int hit = m_random.Next(30, 55);
            return hit;
        }
        private float OnGoldDropRate(int lv)
        {
            int hit = m_random.Next(30, 55);
            return hit;
        }
        private float OnRecovery(int lv)
        {
            //int hit = m_random.Next(8, 13);
            //int hit = m_random.Next(5, 12);
            int hit = m_random.Next(8, 15);
            //int hit = m_random.Next(8, 20);
            return (int)Math.Round(hit * (lv + 9) * 0.1f);
        }
        private float OnFindMagicItemRate(int lv)
        {
            int hit = m_random.Next(30, 55);
            return hit;
        }
        private float OnDEFRate(int lv)
        {
            int hit = m_random.Next(5, 35);
            return hit;
        }
        private float OnHPRate(int lv)
        {
            int hit = m_random.Next(5, 25);
            return hit;
        }
        private float OnRecoveryRate(int lv)
        {
            int hit = m_random.Next(2, 15);
            return hit;
        }

        private float OnAcDMGToRareMon(int lv)
        {
            int hit = m_random.Next(1, 50);
            return hit;
        }

        private float OnAcAllResistRate(int lv)
        {
            int hit = m_random.Next(1, 30);
            return hit;
        }

        private float OnAcHPRate(int lv)
        {
            int hit = m_random.Next(1, 30);
            return hit;
        }
        //private float OnAcDEFRate(int lv)
        //{
        //    int hit = m_random.Next(1, 20);
        //    return hit;
        //}
        private float OnAcRecoveryRate(int lv)
        {
            int hit = m_random.Next(1, 40);
            return hit;
        }


        private float OnEpCriRate(int lv)           { return OnCriRate(lv) * 2; }
        private float OnEpCriDamageRate(int lv)     { return OnCriDamageRate(lv) * 2; }
        private float OnEpResistAll(int lv)         { return OnResistAll(lv) * 2; }
        private float OnEpResistFire(int lv)        { return OnResistFire(lv) * 2; }
        private float OnEpResistIce(int lv)         { return OnResistIce(lv) * 2; }
        private float OnEpResistNature(int lv)      { return OnResistNature(lv) * 2; }
        private float OnEpResistNone(int lv)        { return OnResistNone(lv) * 2; }
        private float OnEpEDFire(int lv)            { return OnEDFire(lv) * 2; }
        private float OnEpEDIce(int lv)             { return OnEDIce(lv) * 2; }
        private float OnEpEDNature(int lv)          { return OnEDNature(lv) * 2; }
        private float OnEpEDNone(int lv)            { return OnEDNone(lv) * 2; }
        private float OnEpEDRateFire(int lv)        { return OnEDRateFire(lv) * 2; }
        private float OnEpEDRateIce(int lv)         { return OnEDRateIce(lv) * 2; }
        private float OnEpEDRateNature(int lv)      { return OnEDRateNature(lv) * 2; }
        private float OnEpEDRateNone(int lv)        { return OnEDRateNone(lv) * 2; }
        private float OnEpDEF(int lv)               { return OnDEF(lv) * 2; }
        private float OnEpHP(int lv)                { return OnHP(lv) * 2; }
        private float OnEpBWDMin(int lv)            { return OnBWDMin(lv) * 2; }
        private float OnEpBWDMax(int lv)            { return OnBWDMax(lv) * 2; }
        private float OnEpWD(int lv)                { return OnWD(lv) * 2; }
        private float OnEpWDRate(int lv)            { return OnWDRate(lv) * 2; }
        private float OnEpAS(int lv)                { return OnAS(lv) * 2; }
        private float OnEpASRate(int lv)            { return OnASRate(lv) * 2; }
        private float OnEpItemDropRate(int lv)      { return OnItemDropRate(lv) * 2; }
        private float OnEpGoldDropRate(int lv)      { return OnGoldDropRate(lv) * 2; }
        private float OnEpRecovery(int lv)          { return OnRecovery(lv) * 2; }
        private float OnEpFindMagicItemRate(int lv) { return OnFindMagicItemRate(lv) * 2; }
        private float OnEpDEFRate(int lv)           { return OnDEFRate(lv) * 2; }
        private float OnEpHPRate(int lv)            { return OnHPRate(lv) * 2; }
        private float OnEpRecoveryRate(int lv)      { return OnRecoveryRate(lv) * 2; }

        private float OnExtraAtkChance(int lv) { return 1; }
        private float OnCrushingBlow(int lv) { return 1; }
        private float OnPlusSetEffect(int lv)
        {
            if (35 <= lv)
                return m_random.Next(1, 3);

            return 1;
        }
        private float OnExtraDMGToRareMon(int lv) { return 1; }
        private float OnLegnendThornRate(int lv) { return 200; }
        private float OnLegnendPoisonRate(int lv) { return 200; }
        private float OnLegnendBurnRate(int lv) { return 200; }
        private float OnLegnendFreezeRate(int lv) { return 5; }
        //private float OnLegnendDMGReduceRate(int lv) { return 10; }
        private float OnLegnendDMGReduceRate(int lv) { return 9; }


        private float OnSETAttack           (int lv) { return 1; }
        private float OnSETLuck             (int lv) { return 1; }
        private float OnSETFindMagicItemRate(int lv) { return 1; }
        private float OnSETExpRate          (int lv) { return 1; }
        private float OnSETFire             (int lv) { return 1; }
        private float OnSETIce              (int lv) { return 1; }
        private float OnSETNature           (int lv) { return 1; }
        private float OnSETNone             (int lv) { return 1; }
        private float OnSETThorn            (int lv) { return 1; }
        private float OnSETPoison           (int lv) { return 1; }
        private float OnSETExtraStone       (int lv) { return 1; }
        private float OnSETRecovery         (int lv) { return 1; }
        private float OnSETHP               (int lv) { return 1; }
        private float OnSETBurn             (int lv) { return 1; }
        private float OnSETFreeze           (int lv) { return 1; }

        private float OnSETFireT3  (int lv) { return 1; }
        private float OnSETIceT3   (int lv) { return 1; }
        private float OnSETNatureT3(int lv) { return 1; }
        private float OnSETNoneT3(int lv) { return 1; }

        //-----------------------------------------------------------------
        // Ancient
        //-----------------------------------------------------------------

        public float GetAncientValue(int lv, eOption option)
        {
            return m_valuesAncient[option](lv);
        }

        private float OnAncientCriRate          (int lv)        { return (float)Math.Round(OnCriRate(lv) * 1.2, 2); }
        private float OnAncientCriDamageRate    (int lv)        { return (float)Math.Round(OnCriDamageRate(lv) * 1.2, 2); }
        private float OnAncientResistAll        (int lv)        { return (float)Math.Round(OnResistAll(lv) * 1.2); }
        private float OnAncientResistFire       (int lv)        { return (float)Math.Round(OnResistFire(lv) * 1.2); }
        private float OnAncientResistIce        (int lv)        { return (float)Math.Round(OnResistIce(lv) * 1.2); }
        private float OnAncientResistNature     (int lv)        { return (float)Math.Round(OnResistNature(lv) * 1.2); }
        private float OnAncientResistNone       (int lv)        { return (float)Math.Round(OnResistNone(lv) * 1.2); }
        private float OnAncientEDFire           (int lv)        { return (float)Math.Round(OnEDFire(lv) * 1.2); }
        private float OnAncientEDIce            (int lv)        { return (float)Math.Round(OnEDIce(lv) * 1.2); }
        private float OnAncientEDNature         (int lv)        { return (float)Math.Round(OnEDNature(lv) * 1.2); }
        private float OnAncientEDNone           (int lv)        { return (float)Math.Round(OnEDNone(lv) * 1.2); }
        private float OnAncientEDRateFire       (int lv)        { return (float)Math.Round(OnEDRateFire(lv) * 1.2, 2); }
        private float OnAncientEDRateIce        (int lv)        { return (float)Math.Round(OnEDRateIce(lv) * 1.2, 2); }
        private float OnAncientEDRateNature     (int lv)        { return (float)Math.Round(OnEDRateNature(lv) * 1.2, 2); }
        private float OnAncientEDRateNone       (int lv)        { return (float)Math.Round(OnEDRateNone(lv) * 1.2, 2); }
        private float OnAncientDEF              (int lv)        { return (float)Math.Round(OnDEF(lv) * 1.2); }
        private float OnAncientHP               (int lv)        { return (float)Math.Round(OnHP(lv) * 1.2); }
        private float OnAncientBWDMin           (int lv)        { return (float)Math.Round(OnBWDMin(lv) * 1.2); }
        private float OnAncientBWDMax           (int lv)        { return (float)Math.Round(OnBWDMax(lv) * 1.2); }
        private float OnAncientWD               (int lv)        { return (float)Math.Round(OnWD(lv) * 1.2); }
        private float OnAncientWDRate           (int lv)        { return (float)Math.Round(OnWDRate(lv) * 1.2, 2); }
        private float OnAncientAS               (int lv)        { return (float)Math.Round(OnAS(lv) * 1.2); }
        private float OnAncientASRate           (int lv)        { return (float)Math.Round(OnASRate(lv) * 1.2, 2); }
        private float OnAncientItemDropRate     (int lv)        { return (float)Math.Round(OnItemDropRate(lv) * 1.2, 2); }
        private float OnAncientGoldDropRate     (int lv)        { return (float)Math.Round(OnGoldDropRate(lv) * 1.2, 2); }
        private float OnAncientRecovery         (int lv)        { return (float)Math.Round(OnRecovery(lv) * 1.2); }
        private float OnAncientFindMagicItemRate(int lv)        { return (float)Math.Round(OnFindMagicItemRate(lv) * 1.2, 2); }
        private float OnAncientDEFRate          (int lv)        { return (float)Math.Round(OnDEFRate(lv) * 1.2, 2); }
        private float OnAncientHPRate           (int lv)        { return (float)Math.Round(OnHPRate(lv) * 1.2, 2); }
        private float OnAncientRecoveryRate     (int lv)        { return (float)Math.Round(OnRecoveryRate(lv) * 1.2, 2); }



        private float OnAncientEpCriRate(int lv)            { return OnAncientCriRate           (lv) * 2; }
        private float OnAncientEpCriDamageRate(int lv)      { return OnAncientCriDamageRate     (lv) * 2; }
        private float OnAncientEpResistAll(int lv)          { return OnAncientResistAll         (lv) * 2; }
        private float OnAncientEpResistFire(int lv)         { return OnAncientResistFire        (lv) * 2; }
        private float OnAncientEpResistIce(int lv)          { return OnAncientResistIce         (lv) * 2; }
        private float OnAncientEpResistNature(int lv)       { return OnAncientResistNature      (lv) * 2; }
        private float OnAncientEpResistNone(int lv)         { return OnAncientResistNone        (lv) * 2; }
        private float OnAncientEpEDFire(int lv)             { return OnAncientEDFire            (lv) * 2; }
        private float OnAncientEpEDIce(int lv)              { return OnAncientEDIce             (lv) * 2; }
        private float OnAncientEpEDNature(int lv)           { return OnAncientEDNature          (lv) * 2; }
        private float OnAncientEpEDNone(int lv)             { return OnAncientEDNone            (lv) * 2; }
        private float OnAncientEpEDRateFire(int lv)         { return OnAncientEDRateFire        (lv) * 2; }
        private float OnAncientEpEDRateIce(int lv)          { return OnAncientEDRateIce         (lv) * 2; }
        private float OnAncientEpEDRateNature(int lv)       { return OnAncientEDRateNature      (lv) * 2; }
        private float OnAncientEpEDRateNone(int lv)         { return OnAncientEDRateNone        (lv) * 2; }
        private float OnAncientEpDEF(int lv)                { return OnAncientDEF               (lv) * 2; }
        private float OnAncientEpHP(int lv)                 { return OnAncientHP                (lv) * 2; }
        private float OnAncientEpBWDMin(int lv)             { return OnAncientBWDMin            (lv) * 2; }
        private float OnAncientEpBWDMax(int lv)             { return OnAncientBWDMax            (lv) * 2; }
        private float OnAncientEpWD(int lv)                 { return OnAncientWD                (lv) * 2; }
        private float OnAncientEpWDRate(int lv)             { return OnAncientWDRate            (lv) * 2; }
        private float OnAncientEpAS(int lv)                 { return OnAncientAS                (lv) * 2; }
        private float OnAncientEpASRate(int lv)             { return OnAncientASRate            (lv) * 2; }
        private float OnAncientEpItemDropRate(int lv)       { return OnAncientItemDropRate      (lv) * 2; }
        private float OnAncientEpGoldDropRate(int lv)       { return OnAncientGoldDropRate      (lv) * 2; }
        private float OnAncientEpRecovery(int lv)           { return OnAncientRecovery          (lv) * 2; }
        private float OnAncientEpFindMagicItemRate(int lv)  { return OnAncientFindMagicItemRate (lv) * 2; }
        private float OnAncientEpDEFRate(int lv)            { return OnAncientDEFRate           (lv) * 2; }
        private float OnAncientEpHPRate(int lv)             { return OnAncientHPRate            (lv) * 2; }
        private float OnAncientEpRecoveryRate(int lv)       { return OnAncientRecoveryRate      (lv) * 2; }

    }
}

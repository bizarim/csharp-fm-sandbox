using fmCommon;
using fmServerCommon;
using System;
using System.Web.Script.Serialization;

namespace appGameServer
{
    public class Msg_Log_IAB : IMessage
    {
        private string m_strConn = string.Empty;
        private rdIABLog m_log = null;

        public Msg_Log_IAB(string strConn, long accid, fmDataShop data)
        {
            m_strConn = strConn;
            m_log = new rdIABLog
            {
                AccId = accid,
                Time = fmServerTime.Now,
                Shop = data.m_eShop,
                Amount = data.m_nAmount,
                Cash = data.m_fNeed,
            };
        }

        public override void Process()
        {
            //using (urq_SetIABLog query = new urq_SetIABLog(eRedis.Log))
            //{
            //    query.i_rdLog = m_log;
            //    query.Execute();
            //}

            using (usp_LogIAB query = new usp_LogIAB(m_strConn))
            {
                query.i_dateTime = m_log.Time;
                query.i_biAccId = m_log.AccId;
                query.i_Shop = m_log.Shop;
                query.i_nAmount = m_log.Amount;
                query.i_fCash = m_log.Cash;

                query.Execute();
            }
        }
        protected override void Release()
        {
            m_log = null;
        }
        public override void Exclude()
        {
        }
    }
}


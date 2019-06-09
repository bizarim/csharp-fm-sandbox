using fmCommon;
using fmServerCommon;
using System;
using System.Web.Script.Serialization;

namespace appGameServer
{
    public class Msg_Log_Ruby : IMessage
    {
        private string m_strConn = string.Empty;
        private rdRubyLog m_log = null;

        public Msg_Log_Ruby(string strConn, long accid, int amount, eProtocolType eProtocolType)
        {
            m_strConn = strConn;
            m_log = new rdRubyLog
            {
                AccId = accid,
                Type = eProtocolType,
                Time = fmServerTime.Now,
                Amount = amount,
            };
        }

        public override void Process()
        {
            //using (urq_SetRubyLog query = new urq_SetRubyLog(eRedis.Log))
            //{
            //    query.i_rdLog = m_log;
            //    query.Execute();
            //}

            using (usp_LogRuby query = new usp_LogRuby(m_strConn))
            {
                query.i_dateTime = m_log.Time;
                query.i_biAccId = m_log.AccId;
                query.i_eProtocolType = m_log.Type;
                query.i_nAmount = m_log.Amount;


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

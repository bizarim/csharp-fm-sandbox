using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    public class Msg_Log_RQ : IMessage
    {
        private string m_strConn = string.Empty;
        private eProtocolType m_eProtocolType = eProtocolType.PT_Unkwon;

        public Msg_Log_RQ(string strConn, eProtocolType packtType)
        {
            m_strConn = strConn;
            m_eProtocolType = packtType;
        }

        public override void Process()
        {

            using (usp_LogRQ query = new usp_LogRQ(m_strConn))
            {
                query.i_dateTime = fmServerTime.Date;
                query.i_eProtocolType = m_eProtocolType;
                query.Execute();
            }
            
        }
        protected override void Release()
        {
        }
        public override void Exclude()
        {
        }
    }
}


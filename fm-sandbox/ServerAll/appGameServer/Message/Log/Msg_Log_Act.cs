using fmCommon;
using fmServerCommon;

namespace appGameServer
{
    public class Msg_Log_Act : IMessage
    {
        private string m_strConn = string.Empty;
        private fmLogAct m_logAct = null;

        public Msg_Log_Act(string strConn, fmLogAct logAct)
        {
            m_strConn = strConn;
            m_logAct = logAct;
        }

        public override void Process()
        {
            if (m_logAct == null)
                return;

            using (usp_LogAct query = new usp_LogAct(m_strConn))
            {
                query.i_logAct = m_logAct;
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


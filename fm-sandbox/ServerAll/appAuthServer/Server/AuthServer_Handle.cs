using fmLibrary;
using fmCommon;
using fmServerCommon;
using System;

namespace appAuthServer
{
    /// <summary>
    /// 인증 서버 메시지 처리부
    /// </summary>
    public partial class AuthServer : appServer
    {
        // Session이 Message를 받았을 때 처리
        protected void OnHandleMessage(SessionBase session, byte[] buffer, int offset, int length)
        {
            //TestStopWatch tsw = new TestStopWatch();
            //tsw.Start();
            try
            {
                m_messageExecuter.Dispatch(session, new Packet(buffer, offset, length));
            }
            catch (Exception ex)
            {
                Logger.Error("Failed! OnHandleMessage", ex);
            }

            //tsw.Stop();
        }
    }
}

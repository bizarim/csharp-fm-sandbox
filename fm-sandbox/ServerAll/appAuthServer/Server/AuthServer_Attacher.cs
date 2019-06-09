using fmLibrary;
using fmCommon;
using fmServerCommon;

namespace appAuthServer
{
    public partial class AuthServer : appServer
    {
        /// <summary>
        /// 센터서버로 프로토콜 발송
        /// </summary>
        /// <param name="fp"></param>
        public void SendPacketToCenter(fmProtocol fp)
        {
            if (null == m_atchCenter)
            {
                Logger.Error("m_atchCenter == null");
                return;
            }

            m_atchCenter.SendPacket(fp);
        }
    }
}

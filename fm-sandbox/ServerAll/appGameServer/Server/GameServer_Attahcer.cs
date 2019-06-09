using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    public partial class GameServer : appServer
    {
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

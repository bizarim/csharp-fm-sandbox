using Compress;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appChatServer.Message
{
    /// <summary>
    /// 방송 노티
    /// </summary>
    public class Msg_GC_Broadcast_Public_NT : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_GC_Broadcast_Public_NT(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var broadcast = new LZ4_PT_GC_Broadcast_Public_NT())
            {
                broadcast.Deserialize(m_recvPacket);

                using (var p = new Packet())
                {
                    broadcast.Serialize(p);
                    //Send(p.GetBuffer(), 0, p.GetPacketLen());
                    ClientSessionManager.Instance.RelayPacet(p);
                }

                //ClientSessionManager.Instance.Broadcasting(broadcast);
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

using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Collections.Generic;

namespace appCenterServer
{
    /// <summary>
    /// 방송 설정 RS
    /// </summary>
    public class Msg_OA_Broadcast_SetNotice_RS : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_OA_Broadcast_SetNotice_RS(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            fmOtherServer op = null;

            if (true == RegisteredServerManager.Instance.TryFindOpServer(out op))
            {
                if (null == op) return;
                if (null == op.m_session) return;
                op.m_session.RelayPacket(m_recvPacket);
            }
        }

        protected override void Release()
        {
            m_server = null;
            m_session = null;
            m_recvPacket = null;
        }
        public override void Exclude()
        {
        }
    }
}

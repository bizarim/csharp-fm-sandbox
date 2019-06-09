using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 서버 등록 message RQ
    /// </summary>
    public class Msg_AC_Server_GetWorldList_RQ : IMessage
    {
        ServerSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_AC_Server_GetWorldList_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ServerSession;
        }

        public override void Process()
        {
            using (var sendfmProtocol = new PT_AC_Server_GetWorldList_RS())
            {
                sendfmProtocol.m_list = RegisteredServerManager.Instance.GetWorldList();
                if (sendfmProtocol.m_list != null)
                    sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                m_session.SendPacket(sendfmProtocol);
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

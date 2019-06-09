using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 서버 등록 완료 후 준비 완료 RQ
    /// </summary>
    public class Msg_Svr_ReadyToStart_RQ : IMessage
    {
        ServerSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Svr_ReadyToStart_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ServerSession;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_Server_ReadyToStart_RQ())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                if (null == m_session.m_descServer)
                {
                    Logger.Error("On_PT_Server_RegisterAtCenter_RQ descOtherServer == null");
                    return;
                }

                m_session.m_descServer.m_eState = eState.eState_Run;

                using (var sendfmProtocol = new PT_Server_ReadyToStart_RS())
                {
                    sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                    m_session.SendPacket(sendfmProtocol);
                }
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

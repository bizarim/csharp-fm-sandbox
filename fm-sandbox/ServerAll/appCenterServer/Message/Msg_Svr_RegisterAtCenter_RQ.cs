using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// 서버 등록 완료 RQ
    /// </summary>
    public class Msg_Svr_RegisterAtCenter_RQ : IMessage
    {
        ServerSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Svr_RegisterAtCenter_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ServerSession;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_Server_RegisterAtCenter_RQ())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                using (var sendfmProtocol = new PT_Server_RegisterAtCenter_RS())
                {
                    var OSD = new descOtherServer
                    {
                        m_nSequence = recvfmProtocol.m_nSequence,
                        m_eServerType = recvfmProtocol.m_eServerType,
                        m_strIP = recvfmProtocol.m_strIP,
                        m_nPort = recvfmProtocol.m_nPort,
                        m_eState = eState.eState_Ready,
                    };

                    m_session.m_descServer = OSD;
                    bool isAdded = RegisteredServerManager.Instance.TryAdd(OSD, m_session);
                    if (true == isAdded)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                        Logger.Info("Success. Registed Server: {0} - Sequnce {1}", OSD.m_eServerType, OSD.m_nSequence);
                    }
                    else
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Server_FailRegister;
                        Logger.Error("Failed. Registed Server: {0} - Sequnce {1}", OSD.m_eServerType, OSD.m_nSequence);
                    }

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

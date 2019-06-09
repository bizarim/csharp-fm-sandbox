using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// MessageDispatcher
    ///     서버에서 처리할 message 분리기
    /// </summary>
    public partial class MessageDispatcher : DispatcherBase
    {
        public MessageDispatcher(appServer server) : base(server) { }

        /// <summary>
        /// 프로토콜 초기화
        ///     사용할 프로토콜 등록
        /// </summary>
        protected override void InitMessage()
        {
            base.InitMessage();

            // center 서버 등록 rs
            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtCenter_RS, Alloc_Msg_Svr_RegisterAtCenter_RS);
            // 서버 등록 rs
            m_dicMessage.Add(eProtocolType.PT_Server_ReadyToStart_RS, Alloc_Msg_Svr_ReadyToStart_RS);
            // 서버 리스트 rs
            m_dicMessage.Add(eProtocolType.PT_AC_Server_GetWorldList_RS, Alloc_Msg_Svr_GetWorldList_RS);
            // 서버 리스트 nt
            m_dicMessage.Add(eProtocolType.PT_CA_Server_GetWorldList_NT, Alloc_Msg_Svr_GetWorldList_NT);
            // 로그인 rq
            m_dicMessage.Add(eProtocolType.PT_CA_Auth_Login_RQ, Alloc_Msg_CA_Auth_Login_RQ);
            // 방송 rq
            m_dicMessage.Add(eProtocolType.PT_CA_Broadcast_GetNotice_RQ, Alloc_Msg_CA_Broadcast_GetNotice_RQ);
            // 방송 설정 rq
            m_dicMessage.Add(eProtocolType.PT_OA_Broadcast_SetNotice_RQ, Alloc_Msg_OA_Broadcast_SetNotice_RQ);
        }

        // message allocate 등록
        // 이렇게 한 이유는 생성자에 params을 강제 하기 위해서
        protected IMessage Alloc_Msg_Svr_RegisterAtCenter_RS(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtCenter_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_ReadyToStart_RS(SessionBase session, Packet packet) { return new Msg_Svr_ReadyToStart_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_GetWorldList_RS(SessionBase session, Packet packet) { return new Msg_Svr_GetWorldList_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_GetWorldList_NT(SessionBase session, Packet packet) { return new Msg_Svr_GetWorldList_NT(m_server, session, packet); }
        protected IMessage Alloc_Msg_CA_Auth_Login_RQ(SessionBase session, Packet packet) { return new Msg_CA_Login_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_CA_Broadcast_GetNotice_RQ(SessionBase session, Packet packet) { return new Msg_CA_Broadcast_GetNotice_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_OA_Broadcast_SetNotice_RQ(SessionBase session, Packet packet) { return new Msg_OA_Broadcast_SetNotice_RQ(m_server, session, packet); }
    }
}

using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appCenterServer
{
    /// <summary>
    /// MessageDispatcher
    ///     서버에서 처리할 message 분리기
    /// </summary>
    public partial class MessageDispatcher : DispatcherBase
    {
        public MessageDispatcher(appServer server) : base(server)
        {

        }
        /// <summary>
        /// 프로토콜 초기화
        ///     사용할 프로토콜 등록
        /// </summary>
        protected override void InitMessage()
        {
            base.InitMessage();

            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtCenter_RQ, Alloc_PT_Server_RegisterAtCenter_RQ);     // 서버 등록 message
            m_dicMessage.Add(eProtocolType.PT_Server_ReadyToStart_RQ, Alloc_PT_Server_ReadyToStart_RQ);             // message 서버 준비 완료
            m_dicMessage.Add(eProtocolType.PT_Server_UpdateWorldState_NT, Alloc_PT_Server_UpdateWorldState_NT);     // message 서버 상태 갱신
            m_dicMessage.Add(eProtocolType.PT_AC_Server_GetWorldList_RQ, Alloc_PT_AC_Server_GetWorldList_RQ);     // message 서버 등록 리스트
            m_dicMessage.Add(eProtocolType.PT_OC_Broadcast_Public_NT,      Alloc_OC_Broadcast_Public_NT    );     // message 방송 노티
            m_dicMessage.Add(eProtocolType.PT_OA_Broadcast_SetNotice_RQ, Alloc_Msg_OA_Broadcast_SetNotice_RQ);     // message 방송 내용 설정 RQ
            m_dicMessage.Add(eProtocolType.PT_OA_Broadcast_SetNotice_RS, Alloc_Msg_OA_Broadcast_SetNotice_RS);     // message 방송 내용 설정 RS
        }

        // message allocate 등록
        // 이렇게 한 이유는 생성자에 params을 강제 하기 위해서
        protected IMessage Alloc_PT_Server_RegisterAtCenter_RQ(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtCenter_RQ(m_server, session, packet); }
        protected IMessage Alloc_PT_Server_ReadyToStart_RQ(SessionBase session, Packet packet) { return new Msg_Svr_ReadyToStart_RQ(m_server, session, packet); }
        protected IMessage Alloc_PT_Server_UpdateWorldState_NT(SessionBase session, Packet packet) { return new Msg_Svr_UpdateWorldState_NT(m_server, session, packet); }
        protected IMessage Alloc_PT_AC_Server_GetWorldList_RQ(SessionBase session, Packet packet) { return new Msg_AC_Server_GetWorldList_RQ(m_server, session, packet); }
        protected IMessage Alloc_OC_Broadcast_Public_NT(SessionBase session, Packet packet) { return new Msg_OC_Broadcast_Public_NT(m_server, session, packet); }
        protected IMessage Alloc_Msg_OA_Broadcast_SetNotice_RQ(SessionBase session, Packet packet) { return new Msg_OA_Broadcast_SetNotice_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_OA_Broadcast_SetNotice_RS(SessionBase session, Packet packet) { return new Msg_OA_Broadcast_SetNotice_RS(m_server, session, packet); }
    }
}

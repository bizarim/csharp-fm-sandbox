using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appChatServer.Message
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
            
            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtCenter_RS, Alloc_Msg_Svr_RegisterAtCenter_RS);   // message 서버 등록 RS 처리 
            m_dicMessage.Add(eProtocolType.PT_Server_ReadyToStart_RS, Alloc_Msg_Svr_ReadyToStart_RS);           // message 서버 준비 완료 RS 처리
            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtChat_RQ, Alloc_Msg_Svr_RegisterAtChat_RQ);       // message 서버 등록 RQ 처리
            m_dicMessage.Add(eProtocolType.PT_GC_Broadcast_Public_NT, Alloc_Msg_GC_Broadcast_Public_NT);        // message 방송 노티 처리
        }

        // message allocate 등록
        // 이렇게 한 이유는 생성자에 params을 강제 하기 위해서
        protected IMessage Alloc_Msg_Svr_RegisterAtCenter_RS(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtCenter_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_ReadyToStart_RS(SessionBase session, Packet packet) { return new Msg_Svr_ReadyToStart_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_RegisterAtChat_RQ(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtChat_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_GC_Broadcast_Public_NT(SessionBase session, Packet packet) { return new Msg_GC_Broadcast_Public_NT(m_server, session, packet); }
    }
}

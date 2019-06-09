using fmCommon;
using fmLibrary;
using fmServerCommon;


namespace appGameServer
{
    /// <summary>
    /// MessageDispatcher
    ///     서버에서 처리할 message 분리기
    /// </summary>
    public partial class MessageDispatcher : DispatcherBase
    {
        public MessageDispatcher(appServer server) : base(server) { }
        protected override void InitMessage()
        {
            base.InitMessage();
            // Svr
            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtCenter_RS,  Alloc_Msg_Svr_RegisterAtCenter_RS);     // 완료
            m_dicMessage.Add(eProtocolType.PT_Server_ReadyToStart_RS,      Alloc_Msg_Svr_ReadyToStart_RS);         // 완료
            m_dicMessage.Add(eProtocolType.PT_Server_RegisterAtChat_RS,    Alloc_Msg_Svr_RegisterAtChat_RS);

            // Lord
            m_dicMessage.Add(eProtocolType.PT_CG_Lord_EnterWorld_RQ,       Alloc_Msg_Auth_EnterWorld_RQ);          // 완료
            m_dicMessage.Add(eProtocolType.PT_CG_Lord_CreateLord_RQ,       Alloc_Msg_Lord_CreateLord_RQ);          // 완료
            m_dicMessage.Add(eProtocolType.PT_CG_Lord_GetLord_RQ,          Alloc_Msg_Lord_GetLord_RQ);             // 완료

            // Item
            m_dicMessage.Add(eProtocolType.PT_CG_Item_GetList_RQ,          Alloc_Msg_Item_GetList_RQ);             // 완료
            m_dicMessage.Add(eProtocolType.PT_CG_Item_Equip_RQ,            Alloc_Msg_Item_Equip_RQ);

            // Shop
            m_dicMessage.Add(eProtocolType.PT_CG_Shop_GetList_RQ,          Alloc_Msg_Shop_GetList_RQ);

            // Mission
            m_dicMessage.Add(eProtocolType.PT_CG_Rank_GetList_RQ,          Alloc_Msg_Rank_GetList_RQ);
            
        }
        // Svr
        protected IMessage Alloc_Msg_Svr_RegisterAtCenter_RS(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtCenter_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_ReadyToStart_RS(SessionBase session, Packet packet) { return new Msg_Svr_ReadyToStart_RS(m_server, session, packet); }
        protected IMessage Alloc_Msg_Svr_RegisterAtChat_RS(SessionBase session, Packet packet) { return new Msg_Svr_RegisterAtChat_RS(m_server, session, packet); }
        // Lord
        protected IMessage Alloc_Msg_Auth_EnterWorld_RQ(SessionBase session, Packet packet) { return new Msg_Lord_EnterWorld_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Lord_CreateLord_RQ(SessionBase session, Packet packet) { return new Msg_Lord_CreateLord_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Lord_GetLord_RQ(SessionBase session, Packet packet) { return new Msg_Lord_GetLord_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Item_GetList_RQ(SessionBase session, Packet packet) { return new Msg_Item_GetList_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Item_Equip_RQ(SessionBase session, Packet packet) { return new Msg_Item_Equip_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Shop_GetList_RQ(SessionBase session, Packet packet) { return new Msg_Shop_GetList_RQ(m_server, session, packet); }
        protected IMessage Alloc_Msg_Rank_GetList_RQ(SessionBase session, Packet packet) { return new Msg_Rank_GetList_RQ(m_server, session, packet); }
       
    }
}

using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    /// <summary>
    /// message 간 Thread 분리 용도
    ///     클라이언트 에서 받은 message를 어디 Thread에서 처리 할지 분리 해주는 처리기
    /// </summary>
    public partial class ThreadSeparator
    {
        /// <summary>
        /// message 기본 처리기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accid"></param>
        protected void PushAtSyncMainRoute(IMessage message, long accid) { SyncMainRoute.Instance.Push(message); }
        /// <summary>
        /// 전투 처리기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accid"></param>
        //protected void PushAtBattleExecuter(IMessage message, long accid) { BattleExecuter.Instance.Push(message); }
        /// <summary>
        /// 업적 처리기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accid"></param>
        protected void PushAtArchiveExecuter(IMessage message, long accid) { ArchiveExecuter.Instance.Push(message); }
        /// <summary>
        /// message 기타 처리기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accid"></param>
        protected void PushAtSyncMessageExecuter(IMessage message, long accid) { SyncMessageExecuter.Instance.Push(accid, message); }
        /// <summary>
        /// 영주 관련 Thread
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accid"></param>
        protected void PushAtAsyncLordCreater(IMessage message, long accid) { AsyncLordCreater.Instance.Push(message); }

        /// <summary>
        /// 분리 설정
        /// </summary>
        protected void InitSeparator()
        {
            // Svr
            m_dicThread.Add(eProtocolType.PT_Server_RegisterAtCenter_RS,   PushAtSyncMainRoute);
            m_dicThread.Add(eProtocolType.PT_Server_ReadyToStart_RS,       PushAtSyncMainRoute);
            m_dicThread.Add(eProtocolType.PT_Server_RegisterAtChat_RS,     PushAtSyncMainRoute);

            m_dicThread.Add(eProtocolType.PT_CG_Auth_GetConstant_RQ,       PushAtSyncMainRoute);
            // Lord
            m_dicThread.Add(eProtocolType.PT_CG_Lord_EnterWorld_RQ,        PushAtSyncMainRoute);
            m_dicThread.Add(eProtocolType.PT_CG_Lord_GetLord_RQ,           PushAtSyncMainRoute);
            m_dicThread.Add(eProtocolType.PT_CG_Lord_CreateLord_RQ,        PushAtAsyncLordCreater);
            // Item
            m_dicThread.Add(eProtocolType.PT_CG_Item_GetList_RQ,           PushAtSyncMessageExecuter);
            m_dicThread.Add(eProtocolType.PT_CG_Item_Equip_RQ,             PushAtSyncMessageExecuter);
            // Shop
            m_dicThread.Add(eProtocolType.PT_CG_Shop_GetList_RQ,           PushAtSyncMessageExecuter);
            m_dicThread.Add(eProtocolType.PT_CG_Rank_GetList_RQ,           PushAtArchiveExecuter);
            m_dicThread.Add(eProtocolType.PT_OC_Broadcast_Public_NT,       PushAtSyncMainRoute);
        }
    }


}

using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    /// <summary>
    /// 아이템 장착
    /// </summary>
    public class Msg_Item_Equip_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Item_Equip_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            // 기본 영주 정보 얻어오기
            fmLord lord = null;
            m_session.TryGetLord(out lord);

            // 프로토콜 RQ
            using (var recvfmProtocol = new PT_CG_Item_Equip_RQ())
            {
                // 프로토콜 Read
                recvfmProtocol.Deserialize(m_recvPacket);

                // 프로토콜 RS
                using (var sendfmProtocol = new PT_CG_Item_Equip_RS())
                {
                    // check
                    if (null == lord)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }
                    // check state
                    if (lord.State != eLordState.Normal)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    // 아이템 장착
                    lord.TryEquip(recvfmProtocol, sendfmProtocol);

                    // 프로토콜 send
                    m_session.SendPacket(sendfmProtocol);

                    if (sendfmProtocol.m_eErrorCode == eErrorCode.Success)
                    {
                        // 로그 남기기
                        ArchiveExecuter.Instance.Push(new Msg_Log_Act(m_server.dbLog(),
                            new fmLogAct
                            {
                                Time = fmServerTime.Now,
                                PType = sendfmProtocol.GeteProtocolType(),
                                AccId = lord.AccId,
                                Lv = lord.GetLv(),
                                Gold = lord.GetGold(),
                                C1 = lord.GetRuby(),
                                C2 = lord.GetStone(),
                            }
                        ));
                    }
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
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }
}

using Compress;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    /// <summary>
    /// 미궁 입장
    /// </summary>
    public class Msg_Lord_EnterWorld_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Lord_EnterWorld_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            // 프로토콜 RQ
            using (var recvfmProtocol = new PT_CG_Lord_EnterWorld_RQ())
            {
                // 프로토콜 Read
                recvfmProtocol.Deserialize(m_recvPacket);

                // 프로토콜 RS
                using (var sendfmProtocol = new PT_CG_Lord_EnterWorld_RS())
                {
                    eErrorCode err = eErrorCode.Success;
                    long accid = 0;
                    // db get token
                    using (var db = new urq_GetToken(eRedis.Token))
                    {
                        db.i_strToken = recvfmProtocol.m_strToken;
                        err = db.Execute();
                        accid = db.o_biAccID;

                        if (eErrorCode.Success != err)
                        {
                            sendfmProtocol.m_eErrorCode = err;
                            m_session.SendPacket(sendfmProtocol);
                            return;
                        }
                    }

                    // 중복 처리
                    fmLord lord = null;
                    if (true == LordManager.Instance.CheckLogin(accid, out lord))
                    {

                        //if (lord.State == eLordState.Maze)
                        //{
                        //    // 유저의 응답 속도는 느려지겠지만 서버의 안정성을 보장 하진 못하나?
                        //    // 쓰레드 분리
                        //    //BattleExecuter.Instance.Push(new Msg_Maze_Delegate_Leave_RQ(m_server, lord));
                        //    return;
                        //}

                        long oldSessionId = lord.SessionId;
                        //Logger.Debug("oldSessionId: {0}", oldSessionId);

                        LordManager.Instance.Logout(lord);

                        if (m_session.GetNumber() != oldSessionId)
                            ClientSessionManager.Instance.OutBeforeSession(oldSessionId);
                    }

                    // db 영주 정보
                    using (var db = new urq_GetLord(eRedis.Game))
                    {
                        db.i_biAccID = accid;
                        err = db.Execute();
                        lord = db.o_lord;
                        
                        if (eErrorCode.Success == err)
                        {
                            if (true == LordManager.Instance.TryAdd(lord))
                            {
                                m_session.SetLord(lord, m_server);
                                lord.GetInformation(sendfmProtocol);
                            }
                        }

                        sendfmProtocol.m_eErrorCode = err;
                        m_session.SendPacket(sendfmProtocol);

                        // 이렇게 한 이유는 패킷을 보내고 방송을 받기 위해서.
                        if (eErrorCode.Success == err)
                        {
                            db.o_lord.State = eLordState.Normal;
                        }
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

using Compress;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    /// <summary>
    /// 영주 생성 요청
    /// </summary>
    public class Msg_Lord_CreateLord_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Lord_CreateLord_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            // 프로토콜 RQ
            using (var recvfmProtocol = new PT_CG_Lord_CreateLord_RQ())
            {
                // 프로토콜 Read
                recvfmProtocol.Deserialize(m_recvPacket);

                // 프로토콜 RS
                using (var sendfmProtocol = new PT_CG_Lord_CreateLord_RS())
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

                    string recvName = recvfmProtocol.m_strName.Trim();
                    err = CheckName(recvName);
                    if (eErrorCode.Success != err)
                    {
                        sendfmProtocol.m_eErrorCode = err;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    // 영주 생성
                    using (var db = new urq_CreateLord(eRedis.Game))
                    {
                        db.i_strName = recvName;
                        db.i_biAccID = accid;
                        err = db.Execute();

                        if (eErrorCode.Success == err)
                        {
                            SyncMainRoute.Instance.Push(new Msg_Delegate_CreateLord_RQ(m_server, m_session, db.o_lord));
                        }
                        else
                        {
                            sendfmProtocol.m_eErrorCode = err;
                            m_session.SendPacket(sendfmProtocol);
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

        private eErrorCode CheckName(string name)
        {
            //Logger.Debug("a Legth {0}", name.Length);

            if (name.Length < 2 || 12 < name.Length)
                return eErrorCode.Lord_NameLegth;

            return eErrorCode.Success;
        }
    }

    public class Msg_Delegate_CreateLord_RQ : IMessage
    {
        private ClientSession m_session = null;
        private fmLord m_lord = null;

        public Msg_Delegate_CreateLord_RQ(appServer server, ClientSession session, fmLord lord)
        {
            m_server = server;
            m_session = session;
            m_lord = lord;
        }

        public override void Process()
        {
            //using (LZ4_PT_CG_Lord_CreateLord_RS sendfmProtocol = new LZ4_PT_CG_Lord_CreateLord_RS())
            using (PT_CG_Lord_CreateLord_RS sendfmProtocol = new PT_CG_Lord_CreateLord_RS())
            {

                LordManager.Instance.TryAdd(m_lord);

                m_session.SetLord(m_lord, m_server);
                m_lord.GetInformation(sendfmProtocol);
                sendfmProtocol.m_eErrorCode = eErrorCode.Success;
                m_session.SendPacket(sendfmProtocol);
            }
        }

        protected override void Release()
        {
            m_server = null;
        }

        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }
}

using System;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    class Msg_Rank_GetList_RQ : IMessage
    {
        ClientSession m_session = null;
        Packet m_recvPacket = null;

        public Msg_Rank_GetList_RQ(appServer server, SessionBase session, Packet packet)
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
            using (var recvfmProtocol = new PT_CG_Rank_GetList_RQ())
            {
                recvfmProtocol.Deserialize(m_recvPacket);

                using (var sendfmProtocol = new PT_CG_Rank_GetList_RS())
                {
                    if (null == lord)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_PleaseLogin;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    using (var db = new urq_GetRank(eRedis.Game))
                    {
                        db.i_rankerKey = new fmRankerKey
                        {
                            AccId = lord.AccId,
                            Name = lord.GetName()
                        };

                        sendfmProtocol.m_eErrorCode = db.Execute();

                        sendfmProtocol.m_list = db.o_rankers;
                        sendfmProtocol.m_nMyRank = db.o_myRank;
                        m_session.SendPacket(sendfmProtocol);
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

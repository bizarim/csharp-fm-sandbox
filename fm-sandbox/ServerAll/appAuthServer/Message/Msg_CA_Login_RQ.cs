using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;

namespace appAuthServer
{
    /// <summary>
    /// 클라이언트에서 인증서버로 로그인 요청 RQ
    /// </summary>
    public class Msg_CA_Login_RQ : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public Msg_CA_Login_RQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            using (var recvfmProtocol = new PT_CA_Auth_Login_RQ())
            {
                // 임시 코드
                try
                {
                    recvfmProtocol.Deserialize(m_recvPacket);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    using (var sendfmProtocol = new PT_CA_Auth_Login_RS())
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_cVerError;
                        m_session.SendPacket(sendfmProtocol);
                    }
                }
                

                using (var sendfmProtocol = new PT_CA_Auth_Login_RS())
                {
                    // 1. 클라 버전 체크

                    if (false == m_server.CheckCver(int.Parse(recvfmProtocol.m_strCver)))
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_cVerError;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    // 2. 서버 버전 체크
                    if (false == m_server.CheckSver(recvfmProtocol.m_strSver))
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_sVerError;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }


                    AuthServer authsvr = m_server as AuthServer;
                    fmWorld gameWolrd = authsvr.m_managerWorld.Clone();

                    if (null == gameWolrd)
                    {
                        sendfmProtocol.m_eErrorCode = eErrorCode.Auth_CheckingServer;
                        m_session.SendPacket(sendfmProtocol);
                        return;
                    }

                    // 3. 로그인
                    using (var db = new usp_Login(m_server.dbAcc()))
                    {
                        db.i_strUniqueKey = recvfmProtocol.m_strUniqueKey;
                        if (false == db.Execute())
                        {
                            sendfmProtocol.m_eErrorCode = eErrorCode.Query_Fail;
                            m_session.SendPacket(sendfmProtocol);
                            return;
                        }

                        eErrorCode err = (eErrorCode)db.o_nResult;
                        long accid = db.o_biAccID;
                        string token = string.Empty;


                        if (eErrorCode.Success == err)
                        {
                            token = m_server.GetToken();
                            using (var reidsQuery = new urq_UpdateAuthToken(eRedis.Token))
                            {
                                reidsQuery.i_biAccID = accid;
                                reidsQuery.i_gameServer = gameWolrd.m_nSequence;
                                reidsQuery.i_strToken = token;
                                err = reidsQuery.Execute();
                            }
                        }

                        // 4. output
                        sendfmProtocol.m_eErrorCode = err;
                        sendfmProtocol.m_strToken = token;
                        sendfmProtocol.m_strIP = gameWolrd.m_strIP;
                        sendfmProtocol.m_nPort = gameWolrd.m_nPort;
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
            // 클라이언트 세션일때
            //m_session.Logout();
        }
    }
}

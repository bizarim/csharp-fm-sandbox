using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;

namespace appChatServer.Server
{
    /// <summary>
    /// 방송 서버 메시지 handler
    /// </summary>
    partial class ChatServer : appServer
    {
        protected void OnHandleMessage(SessionBase session, byte[] buffer, int offset, int length)
        {
            try
            {
                Packet packet = new Packet(buffer, offset, length);

                IMessage msg = null;
                if (true == m_messageExecuter.TryGetMessage(session, packet, out msg))
                {
                    ClientSession client = session as ClientSession;
                    SyncMainRoute.Instance.Push(msg);
                    Logger.Debug("{0}", packet.GeteProtocolType());
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed! OnHandleMessage", ex);
            }

        }

        protected void OnCheckingServer(SessionBase session)
        {
            ServerSession ss = session as ServerSession;
            if (null == ss) return;
            if (null == ss.m_descServer) return;

            Logger.Debug("OnCheckingConnect-Disconnect {0}", ss.m_descServer.m_eServerType);

            if (ss.m_descServer.m_eServerType == eServerType.Center)
            {
                Logger.Warn("Disconnect to CenterServer");
            }
            else if (ss.m_descServer.m_eServerType == eServerType.Game)
            {
                RegisteredServerManager.Instance.Remove(ss.m_descServer);
            }
        }


        public override bool Start()
        {
            if (false == base.Start()) return false;
            if (false == m_netServiceForClient.Start()) return false;
            if (false == m_netServiceForAttach.Start()) return false;

            Logger.Info("{0} -> Start", this.GetType().Name);
            return true;
        }

        public override bool Stop()
        {
            if (false == base.Stop())
                return false;

            return true;
        }
    }
}

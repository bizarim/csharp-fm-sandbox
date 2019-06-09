using fmLibrary;
using fmCommon;
using fmServerCommon;
using System;

namespace appCenterServer
{
    partial class CenterServer : appServer
    {
        protected void OnHandleMessage(SessionBase session, byte[] buffer, int offset, int length)
        {
            try
            {
                m_messageExecuter.Dispatch(session, new Packet(buffer, offset, length));
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

            Logger.Warn(string.Format("OnCheckingConnect-Disconnect {0}", ss.m_descServer.m_eServerType));
            RegisteredServerManager.Instance.Remove(ss.m_descServer);
        }
    }
}

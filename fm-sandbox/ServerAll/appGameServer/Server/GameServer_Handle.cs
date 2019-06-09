using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;

namespace appGameServer
{
    public partial class GameServer : appServer
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
                    m_separator.TrySeparate(packet.GeteProtocolType(), msg, null == client ? 0 : client.GetAccid());
                    //Logger.Debug("{0}-{1}", packet.GeteProtocolType(), null == client ? 0 : client.GetAccid());

                    ArchiveExecuter.Instance.Push(new Msg_Log_RQ(dbLog(), packet.GeteProtocolType()));
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
        }
    }
}

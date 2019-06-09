using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    public class Msg_Session_Add : IMessage
    {
        ClientSession m_session = null;

        public Msg_Session_Add(appServer server, SessionBase session)
        {
            m_server = server;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            //Logger.Debug("Msg_Session_Add start");
            ClientSessionManager.Instance.Add(m_session);
            //Logger.Debug("Msg_Session_Add end");
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;
        }
        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }

    public class Msg_Session_Remove : IMessage
    {
        ClientSession m_session = null;

        public Msg_Session_Remove(appServer server, SessionBase session)
        {
            m_server = server;
            m_session = session as ClientSession;
        }

        public override void Process()
        {
            fmLord lord = null;

            //Logger.Debug("Msg_Session_Remove start");

            if (true == m_session.TryGetLord(out lord))
            {
                //Logger.Debug("TryGetLord lord");

                //if (lord.State == eLordState.Maze)
                //{
                //    //BattleExecuter.Instance.Push(new Msg_Maze_Delegate_Leave(lord));
                //}
                //else
                {
                    LordManager.Instance.Logout(lord);
                }
            }

            //Logger.Debug("Msg_Session_Remove end");
            ClientSessionManager.Instance.Remove(m_session);
        }
        protected override void Release()
        {
            m_server = null;
            m_session = null;
        }
        public override void Exclude()
        {
            if (null != m_session)
                m_session.ForceDisconnect(CloseReason.ThreadExclude);
        }
    }
}

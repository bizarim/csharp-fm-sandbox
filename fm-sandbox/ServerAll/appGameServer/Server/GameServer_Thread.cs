using fmCommon;
using fmLibrary;
using fmServerCommon;
using System;
using System.Threading;

namespace appGameServer
{
    public partial class GameServer : appServer
    {
        // thread
        TickThread m_tickThread;
        TimeChecker m_stateChecker = new TimeChecker(2 * 60);
        TimeChecker m_playerCounter = new TimeChecker(10 * 60);

        //TimeChecker tester = new TimeChecker(7);

        //TimeChecker m_sessionChecker = new TimeChecker(1 * 30 * 60);

        private void Update(object obj)
        {
            if (m_stateChecker.Check())
            {
                int playerCount = ClientSessionManager.Instance.GetCount();

                using (PT_Server_UpdateWorldState_NT sendfmProtocol = new PT_Server_UpdateWorldState_NT())
                {
                    sendfmProtocol.m_eServerType = m_eServerType;
                    sendfmProtocol.m_nSequence = m_config.m_nSequence;
                    sendfmProtocol.m_nPlayerCount = playerCount;
                    //sendfmProtocol.m_eWorldState = GetWorldState(playerCount);

                    SendPacketToCenter(sendfmProtocol);
                }
            }


            if (m_playerCounter.Check())
            {

            }

        }

        private eWorldState GetWorldState(int playercnt)
        {
            int normal = (int)(m_config.m_nMaxConnection * 0.3);
            int busy = (int)(m_config.m_nMaxConnection * 0.7);
            int full = (int)(m_config.m_nMaxConnection * 0.9);

            if (0 < playercnt || playercnt < normal)
            {
                return eWorldState.Normal;
            }
            else if (normal <= playercnt || playercnt < busy)
            {
                return eWorldState.Busy;
            }
            else if (busy <= playercnt)
            {
                return eWorldState.Full;
            }

            return eWorldState.Check;
        }

        public override bool Start()
        {
            ChatSessionManager.Instance.Attach();
            System.Threading.Thread.Sleep(3000);

            if (false == base.Start()) return false;
            if (false == m_netServiceForClient.Start()) return false;
            if (false == m_netServiceForAttach.Start()) return false;

            m_tickThread = new TickThread(this, Update);
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

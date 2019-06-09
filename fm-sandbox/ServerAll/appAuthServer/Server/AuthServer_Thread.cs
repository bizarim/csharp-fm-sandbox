using fmCommon;
using fmLibrary;
using fmServerCommon;
using System.Linq;
using System.Collections.Generic;

namespace appAuthServer
{
    /// <summary>
    /// 인증 서버 Thread 관리부
    /// </summary>
    public partial class AuthServer : appServer
    {
        // thread
        TickThread m_tickThread;
        TimeChecker m_timeChecker = new TimeChecker(1 * 60);

        private void Update(object obj)
        {
            if (false == m_timeChecker.Check())
                return;

            using (PT_AC_Server_GetWorldList_RQ sendfmProtocol = new PT_AC_Server_GetWorldList_RQ())
            {
                m_atchCenter.SendPacket(sendfmProtocol);
            }
        }

        public override bool Start()
        {
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

        public fmWorldManager m_managerWorld = new fmWorldManager();


    }

    public class fmWorldManager
    {
        private readonly object m_objLock = new object();

        public List<fmWorld> m_fmWorldList = new List<fmWorld>();

        public bool TryAdd(List<fmWorld> list)
        {
            lock(m_objLock)
            {
                foreach (var node in m_fmWorldList)
                {
                    node.Dispose();
                }

                m_fmWorldList.Clear();
                m_fmWorldList = null;
                m_fmWorldList = list;
            }

            Logger.Debug("fmWorldManager TryAdd");

            return true;
        }

        public bool TryRemove(fmWorld world)
        {
            lock (m_objLock)
            {
                m_fmWorldList.Remove(world);
            }

            return true;
        }

        public void Clear()
        {
            lock(m_objLock)
            {
                m_fmWorldList.Clear();
            }
        }

        public fmWorld Clone()
        {
            lock (m_objLock)
            {
                //List<fmWorld> list = new List<fmWorld>();

                if (0 < m_fmWorldList.Count)
                {
                    var orderbyList = from x in m_fmWorldList orderby x.m_nPlayer select x;

                    fmWorld world = orderbyList.ElementAt(0);

                    return world.Clone() as fmWorld;
                }

                return null;
            }
        }

        //public List<fmWorld> Clone()
        //{
        //    lock (m_objLock)
        //    {
        //        List<fmWorld> list = new List<fmWorld>();

        //        if (0 < m_fmWorldList.Count)
        //        {
        //            var orderbyList = from x in m_fmWorldList orderby x.m_nPlayer select x;

        //            fmWorld world = orderbyList.ElementAt(0);
        //            list.Add(world.Clone() as fmWorld);
        //        }

        //        return list;
        //    }
        //}
    }
}

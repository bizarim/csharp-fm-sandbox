//using fmCommon;
//using fmServerCommon;
//using System;
//using System.Collections.Generic;

//namespace appGameServer
//{
//    public class LordMission : IDisposable
//    {
//        fmMissionBase m_base = null;
//        List<rdMission> m_dailymissions = null;






//        protected bool m_disposed;
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        ~LordMission()
//        {
//            Dispose(false);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (m_disposed) return;
//            if (disposing)
//            {
//                if (null != m_base)
//                {
//                    m_base = null;
//                }

//                if (null != m_dailymissions)
//                {
//                    m_dailymissions = null;
//                }
//            }
//            m_disposed = true;
//        }
//    }
//}


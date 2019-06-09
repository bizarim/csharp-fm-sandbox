using fmCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    public class LordDiscovery : IDisposable
    {
        protected byte m_checksum = 0;
        protected List<fmDiscovery> m_discoverys = new List<fmDiscovery>();

        public bool TryGetDiscovery(out List<fmDiscovery> discoverys)
        {
            discoverys = m_discoverys;
            return true;
        }

        public bool CheckSum(byte sumBytes)
        {
            if (m_checksum != sumBytes)
                return false;

            return true;
        }

        public void SetCheckSum(byte sumBytes)
        {
            m_checksum = sumBytes;
        }

        public void Add(fmDiscovery discovery)
        {
            m_discoverys.Add(discovery);
        }

        public fmDiscovery Find(int index)
        {
            return m_discoverys.Find(x => x.Index == index);
        }

        public void Clear()
        {
            foreach (var node in m_discoverys)
            {
                node.Dispose();
            }

            m_discoverys.Clear();
        }

        public bool Check()
        {
            if (null == m_discoverys || m_discoverys.Count <= 0)
                return false;

            return true;
        }

        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LordDiscovery()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (null != m_discoverys)
                {
                    foreach (var node in m_discoverys)
                    {
                        node.Dispose();
                    }

                    m_discoverys.Clear();
                    m_discoverys = null;
                }

                m_checksum = 0;
            }
            m_disposed = true;
        }
    }
}

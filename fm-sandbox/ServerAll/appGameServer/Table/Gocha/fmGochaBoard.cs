using fmCommon;
using System;
using System.Collections.Generic;

namespace appGameServer.Table
{
    public class fmGochaNode
    {
        public fmData m_fmData = null;

        public int m_nGochaValue = 0;

        public int m_nBegin = 0;
        public int m_nEnd = 0;
    }

    public abstract class fmGochaBoard
    {
        protected readonly int m_nRate = 1;
        protected int m_nMinVal = 0;
        protected int m_nMaxVal = 0;

        protected Random m_random = null;
        protected List<fmGochaNode> m_listBoard = new List<fmGochaNode>();

        public abstract void Add(fmData _fmData);
        public abstract fmData GochaData();
        public abstract int Gocha();
    }
}

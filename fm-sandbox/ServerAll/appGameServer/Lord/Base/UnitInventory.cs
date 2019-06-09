using fmCommon;
using System;
using System.Collections.Generic;

namespace appGameServer
{
    //public class UnitInventory : IDisposable
    //{
    //    private readonly object m_objLock = new object();

    //    private readonly int MaxStack = 9999;
    //    protected List<rdUnit> m_rdUnits = null;

    //    public bool Initialize(List<rdUnit> list)
    //    {
    //        lock (m_objLock)
    //        {
    //            m_rdUnits = list;
    //        }
    //        return true;
    //    }

    //    public bool TryGetUnits(out List<rdUnit> units)
    //    {
    //        units = null;
    //        if (null == m_rdUnits) return false;
    //        units = m_rdUnits;
    //        return true;
    //    }

    //    public eErrorCode Recruit(int code, int amount, out rdUnit unit)
    //    {
    //        unit = m_rdUnits.Find(x => x.Code == code);
    //        if (unit == null)
    //        {
    //            unit = new rdUnit { Code = code, Amount = amount };
    //            m_rdUnits.Add(unit);
    //        }
    //        else
    //        {
    //            unit.Amount += amount;
    //        }

    //        if (MaxStack < unit.Amount)
    //            return eErrorCode.Unit_MaxRecruit;

    //        return eErrorCode.Success;
    //    }

    //    public eErrorCode Dismiss(int code, int amount)
    //    {
    //        rdUnit unit = m_rdUnits.Find(node => node.Code == code);
    //        if (unit == null)
    //        {
    //            return eErrorCode.Unit_NoneExist;
    //        }

    //        int sum = unit.Amount - amount;

    //        if (sum < 0)
    //            return eErrorCode.Unit_NotEnough;

    //        if (sum == 0)
    //        {
    //            if (false == m_rdUnits.Remove(unit))
    //                return eErrorCode.Auth_PleaseLogin;
    //        }

    //        unit.Amount = sum;

    //        return eErrorCode.Success;
    //    }

    //    public eErrorCode ToInven(int code, int amount)
    //    {
    //        rdUnit unit = m_rdUnits.Find(x => x.Code == code);
    //        if (unit == null)
    //        {
    //            unit = new rdUnit { Code = code, Amount = amount };
    //            m_rdUnits.Add(unit);
    //        }
    //        else
    //        {
    //            unit.Amount += amount;
    //        }

    //        return eErrorCode.Success;
    //    }

    //    public eErrorCode ToPlatoon(int code, int amount)
    //    {
    //        rdUnit unit = m_rdUnits.Find(node => node.Code == code);
    //        if (unit == null)
    //        {
    //            return eErrorCode.Unit_NoneExist;
    //        }

    //        int sum = unit.Amount - amount;

    //        if (sum < 0)
    //            return eErrorCode.Unit_NotEnough;

    //        if (sum == 0)
    //        {
    //            if (false == m_rdUnits.Remove(unit))
    //                return eErrorCode.Auth_PleaseLogin;
    //        }

    //        unit.Amount = sum;

    //        return eErrorCode.Success;
    //    }

    //    protected bool m_disposed;
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    ~UnitInventory()
    //    {
    //        Dispose(false);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (m_disposed) return;
    //        if (disposing)
    //        {
    //            if (m_rdUnits != null)
    //            {
    //                foreach (var node in m_rdUnits) { node.Dispose(); }
    //                m_rdUnits.Clear();
    //                m_rdUnits = null;
    //            }
    //        }
    //        m_disposed = true;
    //    }
    //}
}

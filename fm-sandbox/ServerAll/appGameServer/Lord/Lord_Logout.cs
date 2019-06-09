using fmLibrary;
using System;

namespace appGameServer
{
    /// <summary>
    /// 영주 로그아웃 처리
    /// </summary>
    public partial class fmLord
    {
        public void Logout()
        {
            State = eLordState.Logout;
            Logger.Debug("Lord Logout: accid {0}", AccId);
        }
    }
}

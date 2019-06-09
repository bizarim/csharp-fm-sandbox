using fmCommon;
using fmServerCommon;

namespace appAuthServer
{
    /// <summary>
    /// 인증 서버 config 처리부
    /// </summary>
    public partial class AuthServer : appServer
    {
        public override bool LoadConfig(string[] args)
        {
            // [ MUSTBE BY KWJ ]
            // config 파일 이름 임시 저장. 앱 실행 시 이름 얻어 오도록 하자.
            string[] configfilename = new string[] { "config_Auth.ini" };

            if (false == base.LoadConfig(configfilename))
                return false;

            return true;
        }

        public bool SaveNotice(eLanguage eLang, string contents)
        {
            return true;
        }

        public string GetNotice(eLanguage eLang)
        {
            return string.Empty;
        }

        //public override bool LoadData()
        //{
        //    if (false == base.LoadData())
        //        return false;

        //    return true;
        //}
    }
}

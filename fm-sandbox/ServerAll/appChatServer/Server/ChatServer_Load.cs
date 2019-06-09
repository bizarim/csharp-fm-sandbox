using fmServerCommon;

namespace appChatServer.Server
{
    /// <summary>
    /// 방송 서버 config 로드
    /// </summary>
    partial class ChatServer : appServer
    {
        public override bool LoadConfig(string[] args)
        {
            // [ MUSTBE BY KWJ ]
            // config 파일 이름 임시 저장. 앱 실행 시 이름 얻어 오도록 하자.
            string[] configfilename = new string[] { "config_Chat.ini" };

            if (false == base.LoadConfig(configfilename))
                return false;

            return true;
        }
    }
}

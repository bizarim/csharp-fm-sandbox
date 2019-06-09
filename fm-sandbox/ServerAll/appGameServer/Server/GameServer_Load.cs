using appGameServer.Table;
using fmCommon;
using fmLibrary;
using fmServerCommon;

namespace appGameServer
{
    public partial class GameServer : appServer
    {
        fmDataTableLoader m_fmDataTableLoader = new fmDataTableLoader();

        public override bool LoadData()
        {
            Logger.Info("LoadData -> Start");

            string name = "fmData.fm";
            string path = string.Format(@"{0}\{1}", System.IO.Directory.GetCurrentDirectory(), name);
            fmDataTable table = m_fmDataTableLoader.Load(path);

            if (null == table) return false;
            if (false == theFmDataFinder.Load(table)) return false;
            if (false == theGameConst.Load(table, m_config.m_publicChat)) return false;
            if (false == theLordCreater.Load(table)) return false;
            if (false == theDiscoverer.Load(table)) return false;
            if (false == theOptionPicker.Instance.Load(table)) return false;
            if (false == theMissionPicker.Load(table)) return false;
            if (false == theShop.Load(table)) return false;

            if (false == theMapChecker.Instance.Load(table)) return false;
            if (false == theInDunChecker.Instance.Load(table)) return false;
            if (false == theMonsterPicker.Instance.Load(table)) return false;
            if (false == theItemPicker.Instance.Load(table)) return false;
            if (false == LordManager.Instance.Load(table)) return false;
            //if (false == MazeManager.Instance.Load(table)) return false;


            Logger.Info("LoadData -> End");

            return true;
        }

        public override bool LoadConfig(string[] args)
        {
            // [ MUSTBE BY KWJ ]
            // config 파일 이름 임시 저장. 앱 실행 시 이름 얻어 오도록 하자.
            string[] configfilename = new string[] { "config_Game.ini" };

            if (false == base.LoadConfig(configfilename))
                return false;

            return true;
        }
    }
}

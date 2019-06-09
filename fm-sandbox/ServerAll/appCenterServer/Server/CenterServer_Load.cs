using fmServerCommon;

namespace appCenterServer
{
    partial class CenterServer : appServer
    {
        public override bool LoadConfig(string[] args)
        {
            string[] configfilename = new string[] { "config_Center.ini" };

            if (false == base.LoadConfig(configfilename))
                return false;

            return true;
        }

        public override bool LoadData()
        {
            if (false == base.LoadData())
                return false;

            return true;
        }
    }
}

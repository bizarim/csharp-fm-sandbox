using fmLibrary;

namespace appGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerExecuter<GameServer>.Start(args);
        }
    }
}

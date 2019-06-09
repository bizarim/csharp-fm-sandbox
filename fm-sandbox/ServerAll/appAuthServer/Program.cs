using fmLibrary;

namespace appAuthServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerExecuter<AuthServer>.Start(args);
        }
    }
}

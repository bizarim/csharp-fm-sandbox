using appChatServer.Server;
using fmLibrary;

namespace appChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerExecuter<ChatServer>.Start(args);
        }
    }
}

using fmLibrary;
using fmServerCommon;
using System.Net.Sockets;

namespace appChatServer
{
    /// <summary>
    /// 클라이언트 세션
    /// </summary>
    public class ClientSession : Session
    {
        public ClientSession(Socket socket, SocketAsyncEventArgs recvSAEA, SocketAsyncEventArgs sendSAEA, PooledBufferManager pooledBufferManager)
            : base(socket, recvSAEA, sendSAEA, pooledBufferManager)
        {
        }
    }
}

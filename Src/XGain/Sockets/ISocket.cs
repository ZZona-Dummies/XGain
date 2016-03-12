using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace XGain.Sockets
{
    public interface ISocket : IDisposable
    {
        int BufferSize { get; }
        bool Connected { get; }
        Socket InternalSocket { get; }
        EndPoint LocalEndPoint { get; }
        EndPoint RemoteEndPoint { get; }

        ISocket Accept();
        void Bind(IPEndPoint localEndPoint);
        void Connect(IPEndPoint remoteEndPoint);
        void Listen(int backlog);
        int Receive(byte[] buffer);
        Task<int> ReceiveAsync(byte[] packageSizeBuffer);
        int Send(byte[] buffer);
        Task SendAsync(byte[] statusBuffer);
        void Shutdown(SocketShutdown how);
    }
}

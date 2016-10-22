using System;
using System.Net;
using System.Net.Sockets;

namespace XGain.Sockets
{
    public interface ISocket : IDisposable
    {
        int BufferSize { get; set; }
        bool Connected { get; }
        Socket InternalSocket { get; }
        EndPoint LocalEndPoint { get; }
        EndPoint RemoteEndPoint { get; }

        ISocket Accept();
        void Bind(IPEndPoint localEndPoint);
        void Connect(IPEndPoint remoteEndPoint);
        void Listen(int backlog);
        int Receive(byte[] buffer);
        bool ReceiveAsync(SocketAsyncEventArgs args);
        int Send(byte[] buffer);
        bool SendAsync(SocketAsyncEventArgs args);
        void Shutdown(SocketShutdown how);
    }
}

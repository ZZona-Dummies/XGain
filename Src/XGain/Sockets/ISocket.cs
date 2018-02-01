using System;
using System.Collections.Generic;
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

        int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode);

        int Receive(byte[] buffer, int size, SocketFlags socketFlags);

        int Receive(byte[] buffer, SocketFlags socketFlags);

        int Receive(IList<ArraySegment<byte>> buffers);

        int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags);

        int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode);

        bool ReceiveAsync(SocketAsyncEventArgs args);

        int Send(byte[] buffer);

        int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode);

        int Send(byte[] buffer, int size, SocketFlags socketFlags);

        int Send(byte[] buffer, SocketFlags socketFlags);

        int Send(IList<ArraySegment<byte>> buffers);

        int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags);

        int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode);

        bool SendAsync(SocketAsyncEventArgs args);

        void Shutdown(SocketShutdown how);
    }
}
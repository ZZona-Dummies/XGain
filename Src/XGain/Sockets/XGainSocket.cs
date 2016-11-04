using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace XGain.Sockets
{
    public class XGainSocket : ISocket
    {
        public int BufferSize { get; set; } = 8192;

        public bool Connected => _socket.Connected;
        public Socket InternalSocket => _socket;
        public EndPoint LocalEndPoint => _socket.LocalEndPoint;
        public EndPoint RemoteEndPoint => _socket.RemoteEndPoint;

        private readonly Socket _socket;

        public XGainSocket(
            AddressFamily addressFamily,
            SocketType socketType = SocketType.Stream,
            ProtocolType protocolType = ProtocolType.Tcp,
            bool noDelay = true,
            int? buffer = null)
        {
            _socket = new Socket(addressFamily, socketType, protocolType)
            {
                SendBufferSize = buffer ?? BufferSize,
                ReceiveBufferSize = buffer ?? BufferSize,
                NoDelay = noDelay
            };
        }

        public XGainSocket(Socket socket)
        {
            _socket = socket;
            _socket.SendBufferSize = BufferSize;
            _socket.ReceiveBufferSize = BufferSize;
            _socket.NoDelay = true;
        }

        public ISocket Accept()
        {
            Socket client = _socket.Accept();
            return new XGainSocket(client);
        }

        public void Bind(IPEndPoint localEndPoint)
        {
            _socket.Bind(localEndPoint);
        }

        public void Connect(IPEndPoint remoteEndPoint)
        {
            _socket.Connect(remoteEndPoint);
        }

        public void Listen(int backlog)
        {
            _socket.Listen(backlog);
        }

        public int Receive(byte[] buffer)
        {
            return _socket.Receive(buffer);
        }

        public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            return _socket.Receive(buffer, offset, size, socketFlags);
        }

        public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
        {
            return _socket.Receive(buffer, offset, size, socketFlags, out errorCode);
        }

        public int Receive(byte[] buffer, int size, SocketFlags socketFlags)
        {
            return _socket.Receive(buffer, size, socketFlags);
        }

        public int Receive(byte[] buffer, SocketFlags socketFlags)
        {
            return _socket.Receive(buffer, socketFlags);
        }

        public int Receive(IList<ArraySegment<byte>> buffers)
        {
            return _socket.Receive(buffers);
        }

        public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
        {
            return _socket.Receive(buffers, socketFlags);
        }

        public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
        {
            return _socket.Receive(buffers, socketFlags, out errorCode);
        }

        public bool ReceiveAsync(SocketAsyncEventArgs args)
        {
            return _socket.ReceiveAsync(args);
        }

        public int Send(byte[] buffer)
        {
            return _socket.Send(buffer);
        }

        public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            return _socket.Send(buffer, offset, size, socketFlags);
        }

        public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode)
        {
            return _socket.Send(buffer, offset, size, socketFlags, out errorCode);
        }

        public int Send(byte[] buffer, int size, SocketFlags socketFlags)
        {
            return _socket.Send(buffer, size, socketFlags);
        }

        public int Send(byte[] buffer, SocketFlags socketFlags)
        {
            return _socket.Send(buffer, socketFlags);
        }

        public int Send(IList<ArraySegment<byte>> buffers)
        {
            return _socket.Send(buffers);
        }

        public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
        {
            return _socket.Send(buffers, socketFlags);
        }

        public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode)
        {
            return _socket.Send(buffers, socketFlags, out errorCode);
        }

        public bool SendAsync(SocketAsyncEventArgs args)
        {
            return _socket.SendAsync(args);
        }

        public void Shutdown(SocketShutdown how)
        {
            _socket.Shutdown(how);
        }

        public void Dispose()
        {
            _socket.Dispose();
        }
    }
}

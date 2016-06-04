using System.Net;
using System.Net.Sockets;

namespace XGain.Sockets
{
    public class XGainSocket : ISocket
    {
        public int BufferSize => 8192;
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

        public int Send(byte[] buffer)
        {
            return _socket.Send(buffer);
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

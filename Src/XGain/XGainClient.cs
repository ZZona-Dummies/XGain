using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace XGain
{
    public class XGainClient
    {
        private readonly SocketProcessor _processor = new SocketProcessor();
        private readonly IPAddress _ipAddress;
        private readonly int _port;

        public XGainClient(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task SendAsync(byte[] request)
        {
            Socket socket = null;
            try
            {
                Debug.WriteLine("Connecting to the server socket");
                socket = new Socket(SocketType.Stream, ProtocolType.IP);
                await socket.ConnectAsync(_ipAddress, _port).ConfigureAwait(false);

                Debug.WriteLine("Sending request");
                await _processor.SendAsync(socket, request).ConfigureAwait(false);

                //Debug.WriteLine("Waiting for response");
                //response = await _processor.ReceiveAsync(socket).ConfigureAwait(false);
            }
            finally
            {
                socket?.Dispose();
            }
        }
    }
}

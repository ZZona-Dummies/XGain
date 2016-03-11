using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace XGain
{
    public class XGain : IServer
    {
        public event EventHandler<Message> OnNewMessage;

        private readonly TcpListener _listener;

        public XGain(IPAddress ipAddress, int port)
        {
            _listener = new TcpListener(ipAddress, port);
        }

        public async Task Start()
        {
            _listener.Start();

            while (true)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    Task.Factory.StartNew(() =>
                    {
                        ProcessConnection(client);
                    });
                }
                catch (Exception ex)
                {
                }
            }
        }

        private async void ProcessConnection(TcpClient client)
        {
            Message args = new Message();
            using (MemoryStream ms = new MemoryStream())
            {
                using (NetworkStream ns = client.GetStream())
                {
                    while (ns.DataAvailable)
                    {
                        byte[] buffer = new byte[1024*8];

                        int received = ns.Read(buffer, 0, buffer.Length);

                        await ms.WriteAsync(buffer, 0, received);
                    }

                    args.RequestBytes = ms.ToArray();
                }

                var handler = OnNewMessage;
                handler?.Invoke(client, args);
            }
        }

        public void Dispose()
        {
            try
            {
                _listener.Stop();
            }
            catch (SocketException ex)
            {
            }
        }
    }
}

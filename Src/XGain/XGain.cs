using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using XGain.Processing;
using XGain.Sockets;

namespace XGain
{
    public class XGain : IServer
    {
        public event EventHandler<Message> OnNewMessage;
        private readonly Func<IProcessor> _requestProcessorResolver;
        private readonly TcpListener _listener;

        public XGain(IPAddress ipAddress, int port, Func<IProcessor> requestProcessorResolver)
        {
            _requestProcessorResolver = requestProcessorResolver;
            _listener = new TcpListener(ipAddress, port);
        }

        public async Task Start()
        {
            _listener.Start();

            while (true)
            {
                try
                {
                    Socket socket = await _listener.AcceptSocketAsync();
                    ISocket request = new XGainSocket(socket);
                    Task.Factory.StartNew(() =>
                    {
                        ProcessSocketConnection(request);
                    });
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void ProcessSocketConnection(ISocket socket)
        {
            Message args = new Message();

            IProcessor processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            var handler = OnNewMessage;
            handler?.Invoke(socket, args);
        }

        protected async void ProcessTcpCLientConnection(TcpClient client)
        {
            Message args = new Message();
            using (MemoryStream ms = new MemoryStream())
            {
                using (NetworkStream ns = client.GetStream())
                {
                    while (ns.DataAvailable)
                    {
                        byte[] buffer = new byte[1024 * 8];

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

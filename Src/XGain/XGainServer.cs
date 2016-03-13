using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using XGain.Processing;
using XGain.Sockets;

namespace XGain
{
    public class XGainServer : IServer
    {
        public event EventHandler<Message> OnNewMessage;
        private readonly Func<IProcessor<Message>> _requestProcessorResolver;
        private readonly TcpListener _listener;

        public XGainServer(IPAddress ipAddress, int port, Func<IProcessor<Message>> requestProcessorResolver, 50)
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
                    ProcessSocketConnection(request);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async Task StartParralel()
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

            IProcessor<Message> processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            var handler = OnNewMessage;
            handler?.Invoke(socket, args);
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

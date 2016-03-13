using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using XGain.Processing;
using XGain.Sockets;

namespace XGain
{
    public class XGainServer : IServer
    {
        public event EventHandler<Message> OnNewMessage;
        private readonly int _maximumNumberOfClients = Environment.ProcessorCount;
        private int _tasks = 0;
        private readonly Func<IProcessor<Message>> _requestProcessorResolver;
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);

        private readonly TcpListener _listener;

        public XGainServer(IPAddress ipAddress, int port, Func<IProcessor<Message>> requestProcessorResolver, int? numberOfMaximumClients = null)
        {
            _requestProcessorResolver = requestProcessorResolver;
            _listener = new TcpListener(ipAddress, port);

            if (numberOfMaximumClients != null) _maximumNumberOfClients = numberOfMaximumClients.Value;
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
                    Task.Factory.StartNew(async () =>
                    {
                        ProcessSocketConnection(request);
                    });

                    if (_tasks >= _maximumNumberOfClients)
                    {
                        _resetEvent.WaitOne();
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }

        private void ProcessSocketConnection(ISocket socket)
        {
            Interlocked.Increment(ref _tasks);
            Message args = new Message();

            IProcessor<Message> processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            var handler = OnNewMessage;
            handler?.Invoke(socket, args);

            Interlocked.Decrement(ref _tasks);
            _resetEvent.Set();
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

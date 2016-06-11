using System;
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
        public event EventHandler<StartArgs> OnStart;
        public event EventHandler<MessageArgs> OnNewMessage;
        public event EventHandler<ErrorArgs> OnError;

        private readonly Func<IProcessor<MessageArgs>> _requestProcessorResolver;
        private readonly TcpListener _listener;

        public XGainServer(IPAddress ipAddress, int port) : this(ipAddress, port, () => new ProcessorWithLengthPrefix())
        {
        }

        public XGainServer(IPAddress ipAddress, int port, Func<IProcessor<MessageArgs>> requestProcessorResolver)
        {
            _requestProcessorResolver = requestProcessorResolver;
            _listener = new TcpListener(ipAddress, port);
        }

        public async Task Start(CancellationToken token, int? maxDegreeOfParallelism = null)
        {
            _listener.Start();
            RaiseOnStartEvent();

            while (true)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    Socket socket = await _listener.AcceptSocketAsync();
                    Task.Run(() =>
                    {
                        ISocket request = new XGainSocket(socket);
                        ProcessSocketConnection(request);
                    }, token);
                }
                catch (Exception ex)
                {
                    RaiseOnError(ex);
                }
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
                RaiseOnError(ex);
            }
        }

        private void ProcessSocketConnection(ISocket socket)
        {
            MessageArgs args = new MessageArgs(socket);

            IProcessor<MessageArgs> processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            RaiseOnNewMessageEvent(socket, args);
        }

        private void RaiseOnStartEvent()
        {
            var handler = OnStart;
            handler?.Invoke(this, new StartArgs(_listener.LocalEndpoint));
        }

        private void RaiseOnError(Exception ex)
        {
            var handler = OnError;
            handler?.Invoke(this, new ErrorArgs(ex));
        }

        private void RaiseOnNewMessageEvent(ISocket socket, MessageArgs args)
        {
            var handler = OnNewMessage;
            handler?.Invoke(socket, args);
        }
    }
}

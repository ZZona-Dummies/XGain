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

        private readonly TcpListener _listener;
        private readonly IProcessor<MessageArgs> _processor;

        private readonly CancellationTokenSource _cancel = new CancellationTokenSource();

        public XGainServer(IPAddress ipAddress, int port) : this(ipAddress, port, new ProcessorWithLengthPrefix())
        {
        }

        public XGainServer(IPAddress ipAddress, int port, IProcessor<MessageArgs> processor)
        {
            _listener = new TcpListener(ipAddress, port);
            _processor = processor;
        }

        public void Start(int? maxDegreeOfParallelism = null)
        {
            _listener.Start();
            RaiseOnStartEvent();

            CancellationToken token = _cancel.Token;

            Task.Run(async () =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;

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
            }, token);
        }

        public void Stop()
        {
            try
            {
                _cancel.Cancel();
                _listener.Stop();
            }
            catch (SocketException ex)
            {
                RaiseOnError(ex);
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private async void ProcessSocketConnection(ISocket socket)
        {
            MessageArgs args = await _processor.ProcessSocketConnectionAsync(socket);
            RaiseOnNewMessageEvent(socket, args);
        }

        private void RaiseOnStartEvent()
        {
            OnStart?.Invoke(this, new StartArgs(_listener.LocalEndpoint));
        }

        private void RaiseOnError(Exception ex)
        {
            OnError?.Invoke(this, new ErrorArgs(ex));
        }

        private void RaiseOnNewMessageEvent(ISocket socket, MessageArgs args)
        {
            OnNewMessage?.Invoke(socket, args);
        }
    }
}

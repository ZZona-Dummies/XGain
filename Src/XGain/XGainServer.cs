using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using XGain.Messages;
using XGain.Sockets;

namespace XGain
{
    public class XGainServer : IServer
    {
        public event EventHandler<StartArgs> OnStart;

        public event EventHandler<MessageArgs> OnNewMessage;

        public event EventHandler<ErrorArgs> OnError;

        private readonly TcpListener _listener;
        private readonly SocketProcessor _processor = new SocketProcessor();

        private readonly CancellationTokenSource _cancel = new CancellationTokenSource();

        public XGainServer(IPAddress ipAddress, int port)
        {
            _listener = new TcpListener(ipAddress, port);
        }

        public void Start()
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

                        ISocket request = new XGainSocket(socket);
                        await ProcessSocketConnection(request);
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

        private async Task ProcessSocketConnection(ISocket socket)
        {
            var request = await _processor.ReceiveAsync(socket.InternalSocket);
            RaiseOnNewMessageEvent(socket, new MessageArgs(socket, request));
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
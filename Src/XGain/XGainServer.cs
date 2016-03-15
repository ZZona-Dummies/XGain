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
        public event EventHandler OnStart;
        public event EventHandler<Message> OnNewMessage;
        public event EventHandler<ErrorArgs> OnError;

        private readonly Func<IProcessor<Message>> _requestProcessorResolver;
        private readonly TcpListener _listener;

        public XGainServer(IPAddress ipAddress, int port, Func<IProcessor<Message>> requestProcessorResolver)
        {
            _requestProcessorResolver = requestProcessorResolver;
            _listener = new TcpListener(ipAddress, port);
        }

        public async Task StartSynchronously(CancellationToken token)
        {
            _listener.Start();
            RaiseOnStartEvent();

            while (true)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    Socket socket = await _listener.AcceptSocketAsync();
                    ISocket request = new XGainSocket(socket);

                    ProcessSocketConnection(request);
                }
                catch (Exception ex)
                {
                    RaiseOnError(ex);
                }
            }
        }

        public async Task StartParallel(CancellationToken token, int? maxDegreeOfParallelism = null)
        {
            _listener.Start();
            RaiseOnStartEvent();

            int maximumConcurrencyLevel = maxDegreeOfParallelism ?? TaskScheduler.Current.MaximumConcurrencyLevel;
            int workers = 0;
            AutoResetEvent resetEvent = new AutoResetEvent(false);

            while (true)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    if (workers >= maximumConcurrencyLevel)
                        resetEvent.WaitOne();

                    Task<Socket> task = _listener.AcceptSocketAsync();
                    Interlocked.Increment(ref workers);
                    await task.ContinueWith(socket =>
                    {
                        ISocket request = new XGainSocket(socket.Result);
                        ProcessSocketConnection(request);
                        Interlocked.Decrement(ref workers);
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
            Message args = new Message();

            IProcessor<Message> processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            RaiseOnNewMessageEvent(socket, args);
        }

        private void RaiseOnStartEvent()
        {
            var handler = OnStart;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseOnError(Exception ex)
        {
            var handler = OnError;
            handler?.Invoke(this, new ErrorArgs(ex));
        }


        private void RaiseOnNewMessageEvent(ISocket socket, Message args)
        {
            var handler = OnNewMessage;
            handler?.Invoke(socket, args);
        }
    }
}

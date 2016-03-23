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

        private int _workers = 0;

        public XGainServer(IPAddress ipAddress, int port, Func<IProcessor<MessageArgs>> requestProcessorResolver)
        {
            _requestProcessorResolver = requestProcessorResolver;
            _listener = new TcpListener(ipAddress, port);
        }

        public async Task StartSynchronously(CancellationToken token)
        {
            _listener.Start();
            RaiseOnStartEvent(ProcessingType.Synchronously);
            _workers = 1;

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
            RaiseOnStartEvent(ProcessingType.Parallel);

            int maximumConcurrencyLevel = maxDegreeOfParallelism ?? Environment.ProcessorCount;
            AutoResetEvent resetEvent = new AutoResetEvent(false);

            while (true)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    if (_workers >= maximumConcurrencyLevel)
                        resetEvent.WaitOne();

                    Task<Socket> task = _listener.AcceptSocketAsync();
                    Interlocked.Increment(ref _workers);
                    await task.ContinueWith(socket =>
                    {
                        ISocket request = new XGainSocket(socket.Result);
                        ProcessSocketConnection(request);
                        Interlocked.Decrement(ref _workers);
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
            MessageArgs args = new MessageArgs();

            IProcessor<MessageArgs> processor = _requestProcessorResolver();
            processor.ProcessSocketConnection(socket, args);

            RaiseOnNewMessageEvent(socket, args);
        }

        private void RaiseOnStartEvent(ProcessingType processingType)
        {
            var handler = OnStart;
            handler?.Invoke(this, new StartArgs(processingType, _listener.LocalEndpoint));
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

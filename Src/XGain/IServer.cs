using System;
using System.Threading;
using System.Threading.Tasks;

namespace XGain
{
    public interface IServer : IDisposable
    {
        Task Start(CancellationToken token);
        event EventHandler OnStart;
        event EventHandler<Message> OnNewMessage;
        event EventHandler<ErrorArgs> OnError;
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace XGain
{
    public interface IServer : IDisposable
    {
        event EventHandler<StartArgs> OnStart;
        event EventHandler<MessageArgs> OnNewMessage;
        event EventHandler<ErrorArgs> OnError;

        Task StartSynchronously(CancellationToken token);
        Task StartParallel(CancellationToken token, int? maxDegreeOfParallelism = null);
    }
}

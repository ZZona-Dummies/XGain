using System;
using System.Threading.Tasks;

namespace XGain
{
    public interface IServer : IDisposable
    {
        event EventHandler<StartArgs> OnStart;
        event EventHandler<MessageArgs> OnNewMessage;
        event EventHandler<ErrorArgs> OnError;

        Task StartAsync(int? maxDegreeOfParallelism = null);
        void Stop();
    }
}

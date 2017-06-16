using System;
using XGain.Messages;

namespace XGain
{
    public interface IServer : IDisposable
    {
        event EventHandler<StartArgs> OnStart;
        event EventHandler<MessageArgs> OnNewMessage;
        event EventHandler<ErrorArgs> OnError;

        void Start(int? maxDegreeOfParallelism = null);
        void Stop();
    }
}

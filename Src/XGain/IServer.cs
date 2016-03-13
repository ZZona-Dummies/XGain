using System;
using System.Threading.Tasks;

namespace XGain
{
    public interface IServer : IDisposable
    {
        Task Start();
        Task StartParallel();
        event EventHandler<Message> OnNewMessage;
    }
}

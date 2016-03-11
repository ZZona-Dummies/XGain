using System;
using System.Threading.Tasks;

namespace XGain
{
    public interface IServer : IDisposable
    {
        Task Start();
        event EventHandler<Message> OnNewMessage;
    }
}

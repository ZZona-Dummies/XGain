using System;
using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<in T> where T : EventArgs
    {
        Task ProcessSocketConnection(ISocket socket, T args);
    }
}

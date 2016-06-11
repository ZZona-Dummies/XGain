using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<T> where T : MessageArgs
    {
        Task<T> ProcessSocketConnectionAsync(ISocket socket);
    }
}

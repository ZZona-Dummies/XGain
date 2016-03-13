using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<in T> where T : Message
    {
        Task ProcessSocketConnection(ISocket socket, T args);
    }
}

using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor
    {
        Task ProcessSocketConnection(ISocket socket, Message args);
    }
}

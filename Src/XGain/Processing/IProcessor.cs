using System.Buffers;
using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<T> where T : MessageArgs
    {
        ArrayPool<byte> BytesPool { get; }

        Task<T> ProcessSocketConnectionAsync(ISocket socket);
    }
}

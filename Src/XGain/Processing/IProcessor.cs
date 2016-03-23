using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<T> where T : MessageArgs
    {
        void ProcessSocketConnection(ISocket socket, out T args);
    }
}

using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<T> where T : Message
    {
        void ProcessSocketConnection(ISocket socket, T args);
    }
}

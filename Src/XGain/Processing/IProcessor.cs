using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor<in T> where T : MessageArgs
    {
        void ProcessSocketConnection(ISocket socket, T args);
    }
}

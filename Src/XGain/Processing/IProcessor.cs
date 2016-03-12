using XGain.Sockets;

namespace XGain.Processing
{
    public interface IProcessor
    {
        void ProcessSocketConnection(ISocket socket, Message args);
    }
}

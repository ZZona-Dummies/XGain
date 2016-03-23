using System;
using XGain.Sockets;

namespace XGain
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(ISocket client, object userToken, byte[] requestBytes)
        {
            Client = client;
            UserToken = userToken;
            RequestBytes = requestBytes;
        }

        public ISocket Client { get; }
        public object UserToken { get; }
        public byte[] RequestBytes { get; }
    }
}

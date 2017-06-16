using System;
using XGain.Sockets;

namespace XGain.Messages
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(ISocket client, byte[] requestBytes, object userToken = null)
        {
            Client = client;
            RequestBytes = requestBytes;
            UserToken = userToken;
        }

        public ISocket Client { get; }
        public byte[] RequestBytes { get; }
        public object UserToken { get; }
    }
}

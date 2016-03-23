using System;
using XGain.Sockets;

namespace XGain
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs()
        {
        }

        public MessageArgs(ISocket client, object userToken, byte[] requestBytes)
        {
            Client = client;
            UserToken = userToken;
            RequestBytes = requestBytes;
        }

        public ISocket Client { get; set; }
        public object UserToken { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}

using System;
using XGain.Sockets;

namespace XGain.Messages
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(ISocket client, byte[] requestBytes)
        {
            Client = client;
            RequestBytes = requestBytes;
        }

        public ISocket Client { get; }
        public byte[] RequestBytes { get; }
    }
}
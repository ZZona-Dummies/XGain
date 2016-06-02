using System;
using XGain.Sockets;

namespace XGain
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(ISocket client)
        {
            Client = client;
        }

        public ISocket Client { get; }
        public object UserToken { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}

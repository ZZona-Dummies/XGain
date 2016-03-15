using System;
using XGain.Sockets;

namespace XGain
{
    public class MessageArgs : EventArgs
    {
        public ISocket Client { get; set; }
        public object UserToken { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}

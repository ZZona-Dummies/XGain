using System;

namespace XGain
{
    public class Message : EventArgs
    {
        public Message()
        {
        }

        public Message(byte[] requestBytes)
        {
            RequestBytes = requestBytes;
        }

        public byte[] RequestBytes { get; set; }
    }
}

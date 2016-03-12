using System;

namespace XGain
{
    public class Message : EventArgs
    {
        public object[] UserToken { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}

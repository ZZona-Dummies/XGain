using System;
using System.Net;

namespace XGain
{
    public class StartArgs : EventArgs
    {
        public StartArgs()
        {
        }

        public StartArgs(ProcessingType processingType, EndPoint localEndpoint)
        {
            ProcessingType = processingType;
            LocalEndpoint = localEndpoint;
        }

        public EndPoint LocalEndpoint { get; set; }
        public ProcessingType ProcessingType { get; set; }
    }
}

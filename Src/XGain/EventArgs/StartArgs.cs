using System;
using System.Net;

namespace XGain
{
    public class StartArgs : EventArgs
    {
        public StartArgs(ProcessingType processingType, EndPoint localEndpoint)
        {
            ProcessingType = processingType;
            LocalEndpoint = localEndpoint;
        }

        public EndPoint LocalEndpoint { get; }
        public ProcessingType ProcessingType { get; }
    }
}

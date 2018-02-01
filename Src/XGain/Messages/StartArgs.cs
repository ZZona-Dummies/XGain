using System;
using System.Net;

namespace XGain.Messages
{
    public class StartArgs : EventArgs
    {
        public StartArgs(EndPoint localEndpoint)
        {
            LocalEndpoint = localEndpoint;
        }

        public EndPoint LocalEndpoint { get; }
    }
}
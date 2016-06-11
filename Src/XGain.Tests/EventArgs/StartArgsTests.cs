using System;
using System.Net;
using Xunit;

namespace XGain.Tests.EventArgs
{
    public class StartArgsTests
    {
        [Fact]
        public void Ctor_AssingsProperties()
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

            StartArgs args = new StartArgs(endPoint);

            Assert.Equal(endPoint, args.LocalEndpoint);
        }
    }
}

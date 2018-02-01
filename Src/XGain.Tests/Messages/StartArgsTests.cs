using System.Net;
using XGain.Messages;
using Xunit;

namespace XGain.Tests.Messages
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
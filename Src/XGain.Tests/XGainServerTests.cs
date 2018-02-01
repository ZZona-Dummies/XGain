using System.Net;
using Xunit;

namespace XGain.Tests
{
    [Collection("network")]
    public class XGainServerTests
    {
        private readonly IPAddress address = IPAddress.Loopback;
        private const int port = 5000;

        [Fact]
        public void StartEvenIsRaised()
        {
            using (var server = new XGainServer(address, port))
            {
                bool serverStarted = false;

                server.OnStart += (sender, args) => serverStarted = true;

                server.Start();

                Assert.True(serverStarted);
            }
        }
    }
}
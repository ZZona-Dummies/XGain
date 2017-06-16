using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace XGain.Tests
{
    [Collection("network")]
    public class AcceptanceTest
    {
        private readonly IPAddress address = IPAddress.Loopback;
        private const int port = 5000;

        [Fact]
        public async Task ServerCanReceiveMessage()
        {
            bool messageReceived = false;
            using (var server = new XGainServer(address, port))
            {
                server.OnNewMessage += (sender, args) => messageReceived = true;

                server.Start();

                var client = new XGainClient(address, port);
                await client.SendAsync(new byte[10]);

                Assert.True(messageReceived);
            }
        }
    }
}

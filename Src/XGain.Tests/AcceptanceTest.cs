using System.Net;
using Xunit;

namespace XGain.Tests
{
    public class AcceptanceTest
    {
        [Fact]
        public void DoWork()
        {
            var server = new XGainServer(IPAddress.Any, 5000, null);
        }
    }
}

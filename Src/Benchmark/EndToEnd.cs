using BenchmarkDotNet.Attributes;
using System.Net;
using System.Threading.Tasks;
using XGain;

namespace Benchmark
{
    public class EndToEnd
    {
        [Params(128, 1024, 2048)]
        public int Size { get; set; }

        [Params(1, 10, 50)]
        public int Iterations { get; set; }

        private readonly IPAddress address = IPAddress.Loopback;
        private const int port = 5000;
        private byte[] package;

        [GlobalSetup]
        public void Setup()
        {
            package = new byte[Size];
        }

        [Benchmark]
        public async Task Start()
        {
            using (var server = new XGainServer(address, port))
            {
                server.OnNewMessage += (sender, args) => { };
                server.Start();

                var client = new XGainClient(address, port);
                for (int i = 0; i < Iterations; i++)
                {
                    await client.SendAsync(package);
                }
            }
        }
    }
}
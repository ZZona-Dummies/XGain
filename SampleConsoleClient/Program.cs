using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XGain;

namespace SampleConsoleClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StartAsync().GetAwaiter().GetResult();
        }

        private static async Task StartAsync()
        {
            var client = new XGainClient(IPAddress.Loopback, 5000);
            byte[] bytes = GenerateByte(10000).ToArray();
            while (true)
            {
                Thread.Sleep(100);
                await client.SendAsync(bytes).ConfigureAwait(false);
            }
        }

        private static IEnumerable<byte> GenerateByte(int val)
        {
            for (int i = 0; i < val; ++i)
                yield return 255;
        }
    }
}
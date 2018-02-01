using System.Net;
using System.Text;
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
            var bytes = Encoding.UTF8.GetBytes("hello world");
            while (true)
            {
                await client.SendAsync(bytes).ConfigureAwait(false);
            }
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace XGain.Benchmark
{
    public class RecivingTime
    {
        private const int N = 10000;
        private const int port = 5000;

        private readonly byte[] data;

        private readonly IPAddress ip = Dns.GetHostAddressesAsync(Dns.GetHostName()).Result
            .First(a => a.AddressFamily == AddressFamily.InterNetwork);

        private readonly IServer Server = new XGainServer(IPAddress.Any, port);
        private readonly TcpClient client = new TcpClient();
        private Task _serverWorker;

        public RecivingTime()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
            Server.Start(CancellationToken.None, 1);
            client.ConnectAsync(ip, port).Wait();
        }

        [Benchmark]
        public void SendPackages()
        {
            for (int i = 0; i < 100; i++)
            {
                client.GetStream().WriteAsync(data, 0, data.Length).Wait();
            }
        }
    }
}

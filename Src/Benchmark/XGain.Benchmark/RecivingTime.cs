using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace XGain.Benchmark
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class RecivingTime
    {
        private const int N = 10000;
        private readonly byte[] data;

        public RecivingTime()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public void DoWork()
        {
            const int port = 5000;

            IServer server = new XGainServer(IPAddress.Any, port);
            Task ServerWorker = server.Start(CancellationToken.None, 1);

            for (int i = 0; i < 100; i++)
            {
                var client = new TcpClient();
                client.ConnectAsync(IPAddress.Parse("192.168.43.75"), port).Wait();

                client.GetStream().WriteAsync(data, 0, data.Length).Wait();
            }
        }
    }
}

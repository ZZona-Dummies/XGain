using System;
using System.Net;
using System.Threading;
using XGain;
using XGain.Messages;

namespace SampleServer
{
    class Program
    {
        private static readonly ManualResetEventSlim _cancelEvent = new ManualResetEventSlim();

        static void Main(string[] args)
        {
            const int port = 5000;
            var server = new XGainServer(IPAddress.Loopback, port);

            server.OnStart += OnStartup;
            server.OnError += OnError;
            server.OnNewMessage += OnNewMessage;

            Console.WriteLine("Starting server...");
            server.Start();
            Console.WriteLine("Server started");
            Console.WriteLine("Press CTRL + C to stop");
            
            Console.CancelKeyPress += (sender, arg) => Stop();
            _cancelEvent.Wait();
            _cancelEvent.Reset();

            Console.WriteLine("Turning off server...");
            server.Dispose();
            Console.WriteLine("Server is down");
        }

        private static void Stop()
        {
            _cancelEvent.Set();
        }

        private static void OnNewMessage(object sender, MessageArgs e)
        {
            Console.WriteLine($"New request with {e.RequestBytes.Length} bytes");
        }

        private static void OnError(object sender, ErrorArgs e)
        {
            Console.WriteLine($"Error! {e.Exception}");
        }

        private static void OnStartup(object sender, StartArgs e)
        {
            Console.WriteLine($"Server started, listening on {e.LocalEndpoint}");
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string host = "192.168.0.101";
            const int port = 5000;

            byte[] package = Encoding.UTF8.GetBytes(args[0]);
            try
            {
                TcpClient client = new TcpClient();
                client.ConnectAsync(IPAddress.Parse(host), port).Wait();
                NetworkStream stream = client.GetStream();
                stream.Write(package, 0, package.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

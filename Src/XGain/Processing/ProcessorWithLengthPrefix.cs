using System;
using System.IO;
using System.Threading.Tasks;
using XGain.Sockets;

namespace XGain.Processing
{
    public class ProcessorWithLengthPrefix : IProcessor<MessageArgs>
    {
        public async Task ProcessSocketConnection(ISocket client, MessageArgs args)
        {
            byte[] packageSizeBuffer = new byte[sizeof(long)];
            int position = client.Receive(packageSizeBuffer);

            long requestSize = BitConverter.ToInt64(packageSizeBuffer, 0);

            using (MemoryStream ms = new MemoryStream())
            {
                await ms.WriteAsync(packageSizeBuffer, 0, position);
                position = 0;
                while (position != requestSize)
                {
                    byte[] package = new byte[client.BufferSize];

                    int received = client.Receive(package);

                    await ms.WriteAsync(package, 0, received);
                    position += received;
                }

                // send confirmation
                byte[] statusBuffer = BitConverter.GetBytes(ms.Length);
                client.Send(statusBuffer);

                args.RequestBytes = ms.ToArray();
            }
        }
    }
}

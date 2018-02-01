using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace XGain
{
    public class SocketProcessor
    {
        private byte[] sizeBytes = new byte[sizeof(int)],
                       requestBytes;

        public async Task SendAsync(Socket server, byte[] data)
        {
            var sizeBytes = BitConverter.GetBytes(data.Length);
            await SendAllAsync(server, sizeBytes, sizeBytes.Length).ConfigureAwait(false);
            await SendAllAsync(server, data, data.Length).ConfigureAwait(false);
        }

        public async Task<byte[]> ReceiveAsync(Socket socket)
        {
            await ReceiveAllAsync(socket, sizeBytes, sizeBytes.Length).ConfigureAwait(false);
            int packageSize = BitConverter.ToInt32(sizeBytes, 0);

            //Array resize is a expensive method, we will create a new instance for each new receive, GC will destroy this at the end of the func
            //if (requestBytes == null)
            requestBytes = new byte[packageSize];
            /*else
            {
                if (packageSize != requestBytes.Length)
                    Array.Resize(ref requestBytes, packageSize);
            }*/

            await ReceiveAllAsync(socket, requestBytes, packageSize).ConfigureAwait(false);

            return requestBytes;
        }

        private const int BufferSize = 8 * 1024;

        private static async Task SendAllAsync(Socket socket, byte[] data, int count)
        {
            int position = 0;
            while (position != count)
            {
                int size = Math.Min(count - position, BufferSize);
                int sent = await socket.SendAsync(new ArraySegment<byte>(data, position, size), SocketFlags.None).ConfigureAwait(false);
                position += sent;

                Debug.Assert(position <= data.Length);
            }
        }

        private static async Task ReceiveAllAsync(Socket socket, byte[] data, int count)
        {
            int position = 0;
            while (position != count)
            {
                int size = Math.Min(count - position, BufferSize);
                var segment = new ArraySegment<byte>(data, position, size);
                int received = await socket.ReceiveAsync(segment, SocketFlags.None).ConfigureAwait(false);
                position += received;

                Debug.Assert(position <= count);
            }
        }
    }
}
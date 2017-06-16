using NSubstitute;
using XGain.Messages;
using XGain.Sockets;
using Xunit;

namespace XGain.Tests.Messages
{
    public class MessageArgsTests
    {
        [Fact]
        public void Ctor_AssingsProperties()
        {
            ISocket client = Substitute.For<ISocket>();
            byte[] requestBytes = new byte[1024];
            object userToken = new object();

            MessageArgs args = new MessageArgs(client, requestBytes, userToken);

            Assert.Equal(client, args.Client);
            Assert.Equal(requestBytes, args.RequestBytes);
            Assert.Equal(userToken, args.UserToken);
        }

        [Fact]
        public void Ctor_AssingsPropertiesWithDefaultValues()
        {
            ISocket client = Substitute.For<ISocket>();
            byte[] requestBytes = new byte[1024];

            MessageArgs args = new MessageArgs(client, requestBytes);

            Assert.Equal(client, args.Client);
            Assert.Equal(requestBytes, args.RequestBytes);
            Assert.Null(args.UserToken);
        }
    }
}

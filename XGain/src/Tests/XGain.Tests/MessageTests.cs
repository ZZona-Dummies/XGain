using System.Text;
using Xunit;

namespace XGain.Tests
{
    public class MessageTests
    {
        [Fact]
        public void Ctor_InitializeProperties()
        {
            byte[] package = Encoding.UTF8.GetBytes("lorem ipsum");
            Message message = new Message(package);

            Assert.Equal(package, message.RequestBytes);
        }
    }
}

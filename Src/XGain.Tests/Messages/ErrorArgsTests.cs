using System;
using XGain.Messages;
using Xunit;

namespace XGain.Tests.Messages
{
    public class ErrorArgsTests
    {
        [Fact]
        public void Ctor_AssingsProperties()
        {
            Exception ex = new Exception();

            ErrorArgs args = new ErrorArgs(ex);

            Assert.Equal(ex, args.Exception);
        }
    }
}
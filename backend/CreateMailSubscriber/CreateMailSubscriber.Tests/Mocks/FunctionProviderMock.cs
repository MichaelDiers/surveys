namespace CreateMailSubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts.Messages;
    using Xunit;

    internal class FunctionProviderMock : IPubSubProvider<ICreateMailMessage>
    {
        private readonly ICreateMailMessage expectedMessage;

        public FunctionProviderMock(ICreateMailMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(ICreateMailMessage message)
        {
            Assert.Equal(this.expectedMessage.MailType, message.MailType);
            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}

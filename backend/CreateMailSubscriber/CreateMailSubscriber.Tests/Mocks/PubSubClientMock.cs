namespace CreateMailSubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    internal class PubSubClientMock : ISendMailPubSubClient
    {
        public Task PublishAsync(ISendMailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

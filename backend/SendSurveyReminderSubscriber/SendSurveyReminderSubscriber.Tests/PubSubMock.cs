namespace SendSurveyReminderSubscriber.Tests
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class PubSubMock : ICreateMailPubSubClient
    {
        public Task PublishAsync(ICreateMailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

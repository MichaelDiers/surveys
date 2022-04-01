namespace InitializeSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class PubSubMock
        : ICreateMailPubSubClient, ISaveSurveyPubSubClient, ISaveSurveyResultPubSubClient, ISaveSurveyStatusPubSubClient
    {
        public Task PublishAsync(ICreateMailMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(ISaveSurveyMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(ISaveSurveyResultMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(ISaveSurveyStatusMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

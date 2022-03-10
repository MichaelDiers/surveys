namespace InitializeSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;

    public class PubSubMock
        : ICreateMailPubSubClient, ISaveSurveyPubSubClient, ISaveSurveyResultPubSubClient, ISaveSurveyStatusPubSubClient
    {
        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

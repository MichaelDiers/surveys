namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Contracts;

    internal class PubSubClientMock : ISaveSurveyStatusPubSubClient, ISurveyClosedPubSubClient
    {
        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

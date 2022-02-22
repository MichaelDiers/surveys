namespace InitializeSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;

    public class SaveSurveyResultPubSubMock : ISaveSurveyResultPubSub
    {
        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

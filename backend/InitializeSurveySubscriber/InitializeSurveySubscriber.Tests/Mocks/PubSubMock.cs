namespace InitializeSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.PubSub.Contracts;

    public class PubSubMock : IPubSub
    {
        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

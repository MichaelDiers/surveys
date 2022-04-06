namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    internal class PubSubClientMock : ISaveSurveyStatusPubSubClient, ISurveyClosedPubSubClient
    {
        public Task PublishAsync(ISaveSurveyStatusMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(ISurveyClosedMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

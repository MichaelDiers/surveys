namespace MailerService.Test.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class PubSubMock : ISaveSurveyStatusPubSubClient
    {
        public Task PublishAsync(ISaveSurveyStatusMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

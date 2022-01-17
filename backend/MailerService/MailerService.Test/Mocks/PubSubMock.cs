namespace MailerService.Test.Mocks
{
	using System.Threading.Tasks;
	using MailerService.Contracts;

	public class PubSubMock : IPubSub
	{
		public Task PublishUpdateAsync(string surveyId, string participantId, string status)
		{
			return Task.CompletedTask;
		}
	}
}
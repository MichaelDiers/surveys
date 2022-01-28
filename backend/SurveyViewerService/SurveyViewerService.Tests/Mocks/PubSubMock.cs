namespace SurveyViewerService.Tests.Mocks
{
	using System.Threading.Tasks;
	using SurveyViewerService.Contracts;

	public class PubSubMock : IPubSub
	{
		public Task PublishMessageAsync(string surveyId, ISurveySubmitResult surveySubmitResult)
		{
			return Task.CompletedTask;
		}
	}
}
namespace UpdateSurveyStatusService.Tests.Mock
{
	using System.Threading.Tasks;
	using UpdateSurveyStatusService.Contracts;

	public class DatabaseMock : IDatabase
	{
		public Task InsertStatus(string surveyId, string participantId, string status)
		{
			return Task.CompletedTask;
		}
	}
}
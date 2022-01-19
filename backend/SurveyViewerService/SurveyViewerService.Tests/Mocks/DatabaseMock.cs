namespace SurveyViewerService.Tests.Mocks
{
	using System.Threading.Tasks;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

	public class DatabaseMock : IDatabase
	{
		public Task<ISurvey> ReadSurvey(string participantId)
		{
			return Task.FromResult<ISurvey>(new Survey());
		}
	}
}
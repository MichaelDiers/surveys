namespace SurveyViewerService.Tests.Mocks
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

	public class DatabaseMock : IDatabase
	{
		public Task<ISurvey> ReadSurvey(string participantId)
		{
			return Task.FromResult<ISurvey>(new Survey());
		}

		public Task<IEnumerable<ISurveyStatus>> ReadSurveyStatus(string surveyId)
		{
			return Task.FromResult(Enumerable.Empty<ISurveyStatus>());
		}
	}
}
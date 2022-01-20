namespace SurveyViewerService.Tests.Mocks
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using SurveyViewerService.Contracts;

	public class DatabaseMock : IDatabase
	{
		private readonly ISurvey survey;
		private readonly IEnumerable<ISurveyResult> surveyResults;
		private readonly IEnumerable<ISurveyStatus> surveyStatus;

		public DatabaseMock()
			: this(null, null, null)
		{
		}

		public DatabaseMock(
			ISurvey survey,
			IEnumerable<ISurveyResult> surveyResults,
			IEnumerable<ISurveyStatus> surveyStatus)
		{
			this.survey = survey;
			this.surveyResults = surveyResults ?? Enumerable.Empty<ISurveyResult>();
			this.surveyStatus = surveyStatus ?? Enumerable.Empty<ISurveyStatus>();
		}

		public Task<ISurvey> ReadSurvey(string participantId)
		{
			return Task.FromResult(this.survey);
		}

		public Task<IEnumerable<ISurveyResult>> ReadSurveyResults(string surveyId)
		{
			return Task.FromResult(this.surveyResults);
		}

		public Task<IEnumerable<ISurveyStatus>> ReadSurveyStatus(string surveyId)
		{
			return Task.FromResult(this.surveyStatus);
		}
	}
}
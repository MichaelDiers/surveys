namespace SurveyEvaluatorService.Test.Mocks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;
	using Xunit;

	public class DatabaseMock : IDatabase
	{
		private readonly string expectedSurveyId;
		private readonly IEnumerable<ISurveyStatus> status;
		private readonly Survey survey;

		public DatabaseMock()
			: this(null, new Survey(), Enumerable.Empty<ISurveyStatus>())
		{
		}

		public DatabaseMock(string expectedSurveyId, Survey survey, IEnumerable<ISurveyStatus> status)
		{
			this.expectedSurveyId = expectedSurveyId;
			this.survey = survey ?? throw new ArgumentNullException(nameof(survey));
			this.status = status ?? throw new ArgumentNullException(nameof(status));
		}

		public Task<Survey> ReadSurveyAsync(string surveyId)
		{
			if (this.expectedSurveyId != null)
			{
				Assert.Equal(this.expectedSurveyId, surveyId);
			}

			return Task.FromResult(this.survey);
		}

		public Task<IEnumerable<ISurveyStatus>> ReadSurveyStatusAsync(string surveyId)
		{
			if (this.expectedSurveyId != null)
			{
				Assert.Equal(this.expectedSurveyId, surveyId);
			}

			return Task.FromResult(this.status);
		}
	}
}
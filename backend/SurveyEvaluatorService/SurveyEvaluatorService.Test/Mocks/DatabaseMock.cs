namespace SurveyEvaluatorService.Test.Mocks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Contracts;
	using Xunit;

	public class DatabaseMock : IDatabase
	{
		private readonly string expectedSurveyId;
		private readonly IEnumerable<ISurveyStatus> status;

		public DatabaseMock()
			: this(null, Enumerable.Empty<ISurveyStatus>())
		{
		}

		public DatabaseMock(string expectedSurveyId, IEnumerable<ISurveyStatus> status)
		{
			this.expectedSurveyId = expectedSurveyId;
			this.status = status ?? throw new ArgumentNullException(nameof(status));
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
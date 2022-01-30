namespace SurveyEvaluatorService.Test.Logic
{
	using System;
	using System.Linq;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Logic;
	using SurveyEvaluatorService.Model;
	using SurveyEvaluatorService.Test.Mocks;
	using Xunit;

	public class SurveyEvaluatorProviderTests
	{
		[Theory]
		[InlineData("surveyId")]
		public async void Evaluate(string surveyId)
		{
			var surveyResult = new SurveyResult
			{
				SurveyId = surveyId
			};

			await new SurveyEvaluatorProvider(
				new LoggerMock<SurveyEvaluatorProvider>(),
				new DatabaseMock(surveyId, new Survey(), Enumerable.Empty<ISurveyStatus>()),
				new PubSubMock()).Evaluate(surveyResult);
		}

		[Fact]
		public async void EvaluateThrowsArgumentNullExceptionForNull()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(
				() => new SurveyEvaluatorProvider(
					new LoggerMock<SurveyEvaluatorProvider>(),
					new DatabaseMock(),
					new PubSubMock()).Evaluate(null));
		}
	}
}
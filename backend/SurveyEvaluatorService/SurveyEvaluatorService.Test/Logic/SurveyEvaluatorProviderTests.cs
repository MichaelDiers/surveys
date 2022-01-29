namespace SurveyEvaluatorService.Test.Logic
{
	using System;
	using SurveyEvaluatorService.Logic;
	using SurveyEvaluatorService.Test.Mocks;
	using Xunit;

	public class SurveyEvaluatorProviderTests
	{
		[Fact]
		public async void EvaluateThrowsArgumentNullExceptionForNull()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(
				() => new SurveyEvaluatorProvider(new LoggerMock<SurveyEvaluatorProvider>()).Evaluate(null));
		}
	}
}
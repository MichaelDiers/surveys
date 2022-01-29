namespace SurveyEvaluatorService.Logic
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Evaluate a survey result.
	/// </summary>
	public class SurveyEvaluatorProvider : ISurveyEvaluatorProvider
	{
		/// <summary>
		///   Log errors.
		/// </summary>
		private readonly ILogger<SurveyEvaluatorProvider> logger;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyEvaluatorProvider" />.
		/// </summary>
		/// <param name="logger">Access the error logger.</param>
		public SurveyEvaluatorProvider(ILogger<SurveyEvaluatorProvider> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		///   Evaluate the result of a survey for a participant.
		/// </summary>
		/// <param name="surveyResult">The result of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public Task Evaluate(ISurveyResult surveyResult)
		{
			this.logger.LogInformation(JsonConvert.SerializeObject(surveyResult));
			return Task.CompletedTask;
		}
	}
}
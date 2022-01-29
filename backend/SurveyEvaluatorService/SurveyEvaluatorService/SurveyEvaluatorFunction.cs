namespace SurveyEvaluatorService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.Firestore.V1;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Google cloud function that is triggered if a survey result is created in firestore.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class SurveyEvaluatorFunction : ICloudEventFunction<DocumentEventData>
	{
		/// <summary>
		///   Access the application logger.
		/// </summary>
		private readonly ILogger<SurveyEvaluatorFunction> logger;

		/// <summary>
		///   Provider for evaluating survey results.
		/// </summary>
		private readonly ISurveyEvaluatorProvider surveyEvaluatorProvider;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyEvaluatorFunction" />.
		/// </summary>
		/// <param name="logger">The application logger.</param>
		/// <param name="surveyEvaluatorProvider">Provider for evaluating survey results.</param>
		public SurveyEvaluatorFunction(
			ILogger<SurveyEvaluatorFunction> logger,
			ISurveyEvaluatorProvider surveyEvaluatorProvider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.surveyEvaluatorProvider =
				surveyEvaluatorProvider ?? throw new ArgumentNullException(nameof(surveyEvaluatorProvider));
		}

		/// <summary>
		///   Handle the triggered event.
		/// </summary>
		/// <param name="cloudEvent">The triggered event.</param>
		/// <param name="data">The data containing the created survey result.</param>
		/// <param name="cancellationToken">A token for cancellation.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task HandleAsync(CloudEvent cloudEvent, DocumentEventData data, CancellationToken cancellationToken)
		{
			try
			{
				var surveyResult =
					JsonConvert.DeserializeObject<SurveyResult>(JsonConvert.SerializeObject(data.Value.ConvertFields()));
				await this.surveyEvaluatorProvider.Evaluate(surveyResult);
			}
			catch (Exception exception)
			{
				this.logger.LogError(exception, "Unexpected error!");
			}
		}
	}
}
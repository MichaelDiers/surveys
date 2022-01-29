namespace SurveyEvaluatorService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Events.Protobuf.Cloud.Firestore.V1;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	/// <summary>
	///   Google cloud function that is triggered if a survey result is created in firestore.
	/// </summary>
	public class SurveyEvaluatorFunction : ICloudEventFunction<DocumentEventData>
	{
		/// <summary>
		///   Access the application logger.
		/// </summary>
		private readonly ILogger<SurveyEvaluatorFunction> logger;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyEvaluatorFunction" />.
		/// </summary>
		/// <param name="logger">The application logger.</param>
		public SurveyEvaluatorFunction(ILogger<SurveyEvaluatorFunction> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		///   Handle the triggered event.
		/// </summary>
		/// <param name="cloudEvent">The triggered event.</param>
		/// <param name="data">The data containing the created survey result.</param>
		/// <param name="cancellationToken">A token for cancellation.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public Task HandleAsync(CloudEvent cloudEvent, DocumentEventData data, CancellationToken cancellationToken)
		{
			this.logger.LogInformation(JsonConvert.SerializeObject(data.Value.ConvertFields()));
			return Task.CompletedTask;
		}
	}
}
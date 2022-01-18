namespace SaveSurveyResultService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using Microsoft.Extensions.Logging;
	using SaveSurveyResultService.Contracts;

	/// <summary>
	///   Google cloud function for updating the status of a survey or its participants.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class SaveSurveyResultFunction : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   Logger for error messages.
		/// </summary>
		private readonly ILogger<SaveSurveyResultFunction> logger;

		/// <summary>
		///   Provider for updating the survey status or its participants.
		/// </summary>
		private readonly ISaveSurveyResultProvider saveSurveyResultProvider;

		/// <summary>
		///   Creates a new instance of <see cref="SaveSurveyResultFunction" />.
		/// </summary>
		/// <param name="logger">Logger for error messages.</param>
		/// <param name="saveSurveyResultProvider">Provider for processing survey results.</param>
		public SaveSurveyResultFunction(
			ILogger<SaveSurveyResultFunction> logger,
			ISaveSurveyResultProvider saveSurveyResultProvider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.saveSurveyResultProvider =
				saveSurveyResultProvider ?? throw new ArgumentNullException(nameof(saveSurveyResultProvider));
		}

		/// <summary>
		///   Process survey results.
		/// </summary>
		/// <param name="cloudEvent">The cloud event that is handled.</param>
		/// <param name="data">The message data.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public async Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				await this.saveSurveyResultProvider.InsertSurveyResult(data?.Message?.TextData);
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error.");
			}
		}
	}
}
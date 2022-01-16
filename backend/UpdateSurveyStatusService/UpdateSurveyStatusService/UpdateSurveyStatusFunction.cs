namespace UpdateSurveyStatusService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using Microsoft.Extensions.Logging;
	using UpdateSurveyStatusService.Contracts;

	/// <summary>
	///   Google cloud function for updating the status of a survey or its participants.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class UpdateSurveyStatusFunction : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   Logger for error messages.
		/// </summary>
		private readonly ILogger<UpdateSurveyStatusFunction> logger;

		/// <summary>
		///   Provider for updating the survey status or its participants.
		/// </summary>
		private readonly IUpdateProvider updateProvider;

		/// <summary>
		///   Creates a new instance of <see cref="UpdateSurveyStatusFunction" />.
		/// </summary>
		/// <param name="logger">Logger for error messages.</param>
		/// <param name="updateProvider">Provider for updating surveys.</param>
		public UpdateSurveyStatusFunction(ILogger<UpdateSurveyStatusFunction> logger, IUpdateProvider updateProvider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.updateProvider = updateProvider ?? throw new ArgumentNullException(nameof(updateProvider));
		}

		/// <summary>
		///   Update the status of surveys or its participants.
		/// </summary>
		/// <param name="cloudEvent">The cloud event that is handled.</param>
		/// <param name="data">The message data.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public async Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				await this.updateProvider.Update(data?.Message?.TextData);
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error.");
			}
		}
	}
}
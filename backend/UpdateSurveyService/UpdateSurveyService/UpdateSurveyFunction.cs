namespace UpdateSurveyService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///   Google cloud function for sending emails using pub/sub.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class UpdateSurveyFunction : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   Logger for error messages.
		/// </summary>
		private readonly ILogger<UpdateSurveyFunction> logger;

		/// <summary>
		///   Creates a new instance of <see cref="UpdateSurveyFunction" />.
		/// </summary>
		/// <param name="logger">Logger for error messages.</param>
		public UpdateSurveyFunction(ILogger<UpdateSurveyFunction> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		///   Handle cloud events and send email using <see cref="MessagePublishedData" />.
		/// </summary>
		/// <param name="cloudEvent">The cloud event that is handled.</param>
		/// <param name="data">The message data.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				this.logger.LogInformation("Update called");
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error.");
			}

			return Task.CompletedTask;
		}
	}
}
namespace MailerService
{
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	/// <summary>
	///   Google cloud function for sending emails using pub/sub.
	/// </summary>
	public class MailerFunction : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   Logger for error messages.
		/// </summary>
		private readonly ILogger<MailerFunction> logger;

		/// <summary>
		///   Creates a new instance of <see cref="MailerFunction" />.
		/// </summary>
		/// <param name="logger">Logger for error messages.</param>
		public MailerFunction(ILogger<MailerFunction> logger)
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
				var message = JsonConvert.DeserializeObject<Message>(data.Message.TextData);
				this.logger.LogInformation(
					string.Join(", ", message?.Recipients?.Select(x => x.Name) ?? Enumerable.Empty<string>()));
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error.");
			}

			return Task.CompletedTask;
		}
	}
}
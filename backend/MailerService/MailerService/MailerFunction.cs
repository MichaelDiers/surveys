namespace MailerService
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using MailerService.Contracts;
	using MailerService.Model;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	/// <summary>
	///   Google cloud function for sending emails using pub/sub.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class MailerFunction : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   Logger for error messages.
		/// </summary>
		private readonly ILogger<MailerFunction> logger;

		/// <summary>
		///   Provider for sending emails.
		/// </summary>
		private readonly IMailerProvider mailerProvider;

		/// <summary>
		///   Creates a new instance of <see cref="MailerFunction" />.
		/// </summary>
		/// <param name="logger">Logger for error messages.</param>
		/// <param name="mailerProvider">Provider for sending emails.</param>
		public MailerFunction(ILogger<MailerFunction> logger, IMailerProvider mailerProvider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.mailerProvider = mailerProvider ?? throw new ArgumentNullException(nameof(mailerProvider));
			this.mailerProvider.SendAsync(null);
		}

		/// <summary>
		///   Handle cloud events and send email using <see cref="MessagePublishedData" />.
		/// </summary>
		/// <param name="cloudEvent">The cloud event that is handled.</param>
		/// <param name="data">The message data.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public async Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				var message = JsonConvert.DeserializeObject<Message>(data.Message.TextData);
				await this.mailerProvider.SendAsync(message);
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error.");
			}
		}
	}
}
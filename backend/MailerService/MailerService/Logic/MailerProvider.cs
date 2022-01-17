namespace MailerService.Logic
{
	using System;
	using System.Threading.Tasks;
	using MailerService.Contracts;
	using MailerService.Model;
	using MimeKit;
	using Newtonsoft.Json;

	/// <summary>
	///   Provider for sending email messages for surveys.
	/// </summary>
	public class MailerProvider : IMailerProvider
	{
		/// <summary>
		///   The application configuration.
		/// </summary>
		private readonly IMailerServiceConfiguration configuration;

		/// <summary>
		///   Mail for sending emails via smtp.
		/// </summary>
		private readonly IMailerSmtpClient mailerSmtpClient;

		/// <summary>
		///   Converter for <see cref="IMessage" /> to <see cref="MimeMessage" />.
		/// </summary>
		private readonly IMessageConverter messageConverter;

		/// <summary>
		///   Access the Pub/Sub client.
		/// </summary>
		private readonly IPubSub pubSub;

		/// <summary>
		///   Creates a new instance of <see cref="MailerProvider" />.
		/// </summary>
		/// <param name="messageConverter">Converter for <see cref="IMessage" /> to <see cref="MimeMessage" />.</param>
		/// <param name="mailerSmtpClient">Sends mails via smtp.</param>
		/// <param name="configuration">The application configuration.</param>
		/// <param name="pubSub">Access the Pub/Sub client.</param>
		public MailerProvider(
			IMessageConverter messageConverter,
			IMailerSmtpClient mailerSmtpClient,
			IMailerServiceConfiguration configuration,
			IPubSub pubSub)
		{
			this.messageConverter = messageConverter ?? throw new ArgumentNullException(nameof(messageConverter));
			this.mailerSmtpClient = mailerSmtpClient ?? throw new ArgumentNullException(nameof(mailerSmtpClient));
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.pubSub = pubSub ?? throw new ArgumentNullException(nameof(pubSub));
		}

		/// <summary>
		///   Sends an email message.
		/// </summary>
		/// <param name="json">A <see cref="Message" /> serialized as json.</param>
		public async Task SendAsync(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentNullException(nameof(json));
			}

			var message = JsonConvert.DeserializeObject<Message>(json);
			if (message == null)
			{
				throw new ArgumentNullException(nameof(json), "Cannot deserialize to Message object.");
			}

			var mimeMessageFrom = new[]
			{
				new MailboxAddress(this.configuration.MailboxAddressFrom.Name, this.configuration.MailboxAddressFrom.Email)
			};

			var email = this.messageConverter.ToMimeMessage(message, mimeMessageFrom, this.configuration.TemplateNewline);

			var success = false;
			try
			{
				await this.mailerSmtpClient.SendAsync(email, this.configuration.Smtp);
				success = true;
			}
			finally
			{
				var status = success ? message.StatusOk : message.StatusFailed;
				foreach (var participantId in message.ParticipantIds)
				{
					await this.pubSub.PublishUpdateAsync(message.SurveyId, participantId, status);
				}
			}
		}
	}
}
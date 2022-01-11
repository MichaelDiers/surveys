namespace MailerService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MailerService.Contracts;
	using MimeKit;

	/// <summary>
	///   Converter for <see cref="IMailerServiceRequest" /> to <see cref="MimeMessage" />.
	/// </summary>
	public class MessageConverter : IMessageConverter
	{
		/// <summary>
		///   Access the application configuration.
		/// </summary>
		private readonly IMailerServiceConfiguration configuration;

		/// <summary>
		///   Creates a new instance of <see cref="MessageConverter" />.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public MessageConverter(IMailerServiceConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///   Convert a <see cref="IMailerServiceRequest" /> to a <see cref="MimeMessage" />.
		/// </summary>
		/// <param name="request">The data that is used to create the <see cref="MimeMessage" />.</param>
		/// <param name="from">The sender data of the email.</param>
		/// <returns>An instance of <see cref="MimeMessage" />.</returns>
		public MimeMessage ToMimeMessage(IMailerServiceRequest request, IEnumerable<InternetAddress> from)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var body = this.CreateBody(request);

			var mimeMessage = new MimeMessage(
				from,
				request.Recipients.Select(r => new MailboxAddress(r.Name, r.Email)),
				request.SurveyName,
				body);

			mimeMessage.ReplyTo.Add(new MailboxAddress(request.ReplyTo.Name, request.ReplyTo.Email));

			return mimeMessage;
		}

		/// <summary>
		///   Create text and html email body.
		/// </summary>
		/// <param name="request">The email data for that the body is created.</param>
		/// <returns>The email body as <see cref="MimeEntity" />.</returns>
		private MimeEntity CreateBody(IMailerServiceRequest request)
		{
			var builder = new BodyBuilder();
			if (request.EmailType == EmailType.SurveyRequest)
			{
				builder.TextBody = string.Format(
					this.configuration.SurveyRequestTemplate.Text,
					string.Join(", ", request.Recipients.Select(r => r.Name)),
					request.SurveyName,
					request.SurveyLink,
					Environment.NewLine);
				builder.HtmlBody = string.Format(
					this.configuration.SurveyRequestTemplate.Html,
					string.Join(", ", request.Recipients.Select(r => r.Name)),
					request.SurveyName,
					request.SurveyLink);
			}
			else if (request.EmailType == EmailType.ThankYou)
			{
				builder.TextBody = string.Format(
					this.configuration.ThankYouTemplate.Text,
					string.Join(", ", request.Recipients.Select(r => r.Name)),
					request.SurveyName,
					string.Join(Environment.NewLine, request.Results),
					Environment.NewLine);
				builder.HtmlBody = string.Format(
					this.configuration.ThankYouTemplate.Html,
					string.Join(", ", request.Recipients.Select(r => r.Name)),
					request.SurveyName,
					string.Join(
						string.Empty,
						request.Results.Select(x => string.Format(this.configuration.ThankYouTemplate.HtmlElement, x))));
			}

			return builder.ToMessageBody();
		}
	}
}
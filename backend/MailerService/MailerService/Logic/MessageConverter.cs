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

			var builder = new BodyBuilder
			{
				TextBody = "my text body",
				HtmlBody = "<html><body><h1>my html body</h1></body></html>"
			};

			var body = builder.ToMessageBody();

			var mimeMessage = new MimeMessage(
				from,
				request.Recipients.Select(r => new MailboxAddress(r.Name, r.Email)),
				request.SurveyName,
				body);

			return mimeMessage;
		}
	}
}
namespace MailerService.Logic
{
	using System;
	using System.Linq;
	using MailerService.Contracts;
	using MailerService.Model;
	using MimeKit;

	/// <summary>
	///   Converter for <see cref="Message" /> instances.
	/// </summary>
	public class MessageConverter : IMessageConverter
	{
		/// <summary>
		///   Convert a <see cref="Message" /> to a <see cref="MimeMessage" />.
		/// </summary>
		/// <param name="message">The data that is used to create the <see cref="MimeMessage" />.</param>
		/// <returns>An instance of <see cref="MimeMessage" />.</returns>
		public MimeMessage ToMimeMessage(Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			var mimeMessage = new MimeMessage();

			// address part
			if (string.IsNullOrWhiteSpace(message.Sender?.Name) || string.IsNullOrWhiteSpace(message.Sender?.Email))
			{
				throw new ArgumentNullException(nameof(message.Sender), "Element or one of its components is missing.");
			}

			mimeMessage.From.Add(new MailboxAddress(message.Sender.Name, message.Sender.Email));

			if (message.Recipients?.Any(x => !x.IsReplyTo) != true)
			{
				throw new ArgumentNullException(nameof(message.Recipients));
			}

			foreach (var messageRecipient in message.Recipients)
			{
				if (string.IsNullOrWhiteSpace(messageRecipient?.Name) || string.IsNullOrWhiteSpace(messageRecipient.Email))
				{
					throw new ArgumentNullException(nameof(message.Recipients), "Element or one of its components is missing.");
				}

				var address = new MailboxAddress(messageRecipient.Name, messageRecipient.Email);
				if (messageRecipient.IsBcc)
				{
					mimeMessage.Bcc.Add(address);
				}
				else if (messageRecipient.IsCc)
				{
					mimeMessage.Cc.Add(address);
				}
				else if (messageRecipient.IsReplyTo)
				{
					mimeMessage.ReplyTo.Add(address);
				}
				else
				{
					mimeMessage.To.Add(address);
				}
			}

			// subject and body part
			if (string.IsNullOrWhiteSpace(message.Subject))
			{
				throw new ArgumentNullException(nameof(message.Subject));
			}

			mimeMessage.Subject = message.Subject;

			if (string.IsNullOrWhiteSpace(message.Body?.TextContent) || string.IsNullOrWhiteSpace(message.Body?.HtmlContent))
			{
				throw new ArgumentNullException(nameof(message.Body));
			}

			var builder = new BodyBuilder
			{
				TextBody = message.Body.TextContent,
				HtmlBody = message.Body.HtmlContent
			};
			mimeMessage.Body = builder.ToMessageBody();

			return mimeMessage;
		}
	}
}
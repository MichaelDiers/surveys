namespace MailerService.Test.Logic
{
	using MailerService.Logic;
	using MailerService.Model;
	using MimeKit;
	using Xunit;

	public class MessageConverterTests
	{
		[Theory]
		[InlineData(
			"recipientEmail",
			"recipientName",
			"replyToName",
			"replyToEmail",
			"subject",
			"textHtml",
			"textPlain",
			"fromName",
			"fromEmail")]
		public void ToMimeMessage(
			string recipientEmail,
			string recipientName,
			string replyToName,
			string replyToEmail,
			string subject,
			string textHtml,
			string textPlain,
			string fromName,
			string fromEmail)
		{
			var message = new MessageConverter().ToMimeMessage(
				new Message
				{
					Recipients = new[]
					{
						new Recipient
						{
							Email = recipientEmail,
							Name = recipientName
						}
					},
					ReplyTo = new Recipient
					{
						Email = replyToEmail,
						Name = replyToName
					},
					Subject = subject,
					Body = new Body
					{
						Html = textHtml,
						Plain = textPlain
					}
				},
				new[]
				{
					new MailboxAddress(fromName, fromEmail)
				},
				"HANDLE");

			Assert.Single(message.To.Mailboxes);
			Assert.Contains(message.To.Mailboxes, mb => mb.Address == recipientEmail && mb.Name == recipientName);
			Assert.Equal(subject, message.Subject);
			Assert.Single(message.From.Mailboxes);
			Assert.Contains(message.From.Mailboxes, mb => mb.Address == fromEmail && mb.Name == fromName);
		}
	}
}
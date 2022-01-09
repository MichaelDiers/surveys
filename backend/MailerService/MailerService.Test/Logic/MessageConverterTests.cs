namespace MailerService.Test.Logic
{
	using MailerService.Contracts;
	using MailerService.Logic;
	using MailerService.Model;
	using MimeKit;
	using Xunit;

	public class MessageConverterTests
	{
		[Theory]
		[InlineData(
			EmailType.SurveyRequest,
			"recipientEmail",
			"recipientName",
			"surveyName",
			"surveyLink",
			"fromName",
			"fromAddress")]
		public void ToMimeMessage(
			EmailType emailType,
			string recipientEmail,
			string recipientName,
			string surveyName,
			string surveyLink,
			string fromName,
			string fromAddress)
		{
			var message = new MessageConverter().ToMimeMessage(
				new MailerServiceRequest
				{
					EmailType = emailType,
					Recipients = new[]
					{
						new Recipient
						{
							Email = recipientEmail,
							Name = recipientName
						}
					},
					SurveyName = surveyName,
					SurveyLink = surveyLink
				},
				new[]
				{
					new MailboxAddress(fromName, fromAddress)
				});

			Assert.Single(message.To.Mailboxes);
			Assert.Contains(message.To.Mailboxes, mb => mb.Address == recipientEmail && mb.Name == recipientName);
			Assert.Equal(surveyName, message.Subject);
			Assert.Single(message.From.Mailboxes);
			Assert.Contains(message.From.Mailboxes, mb => mb.Address == fromAddress && mb.Name == fromName);
		}
	}
}
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
			"fromAddress",
			"replyToName",
			"replyToEmail",
			null,
			null)]
		[InlineData(
			EmailType.ThankYou,
			"recipientEmail",
			"recipientName",
			"surveyName",
			"surveyLink",
			"fromName",
			"fromAddress",
			"replyToName",
			"replyToEmail",
			"result1",
			"result2")]
		public void ToMimeMessage(
			EmailType emailType,
			string recipientEmail,
			string recipientName,
			string surveyName,
			string surveyLink,
			string fromName,
			string fromAddress,
			string replyToName,
			string replyToEmail,
			string results1,
			string results2)
		{
			var message = new MessageConverter(
				new MailerServiceConfiguration
				{
					SurveyRequestTemplate = new MessageTemplate
					{
						Text = "{0}{1}{2}",
						Html = "{0}{1}{2}"
					},
					ThankYouTemplate = new MessageTemplate
					{
						Text = "{0}{1}{2}",
						Html = "{0}{1}{2}",
						HtmlElement = "{0}"
					}
				}).ToMimeMessage(
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
					SurveyLink = surveyLink,
					ReplyTo = new Recipient
					{
						Email = replyToEmail,
						Name = replyToName
					},
					Results = new[]
					{
						results1,
						results2
					}
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
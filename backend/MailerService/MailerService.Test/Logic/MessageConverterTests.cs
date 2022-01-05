namespace MailerService.Test.Logic
{
	using System;
	using System.Linq;
	using MailerService.Contracts;
	using MailerService.Logic;
	using MailerService.Model;
	using Xunit;

	public class MessageConverterTests
	{
		[Fact]
		public void MessageConverterCtor()
		{
			var converter = new MessageConverter();
			Assert.IsAssignableFrom<IMessageConverter>(converter);
		}

		[Fact]
		public void ToMimeMessage()
		{
			const string sender = "sender@email.de";
			const string htmlContent = "<html><body><h1>my html content</h1></body></html>";
			const string textContent = "my text content";
			const string subject = "my cool subject";

			const string recipient1EmailCc1 = "cc1@bar.de";
			const string recipient2EmailCc2 = "cc2@bar.de";
			const string recipient3EmailBcc1 = "bcc1@bar.de";
			const string recipient4EmailBcc2 = "bcc2@bar.de";
			const string recipient5EmailReplyTo1 = "reply1@bar.de";
			const string recipient6EmailReplyTo2 = "reply2@bar.de";
			const string recipient7EmailTo1 = "to1@bar.de";
			const string recipient8EmailTo2 = "to2@bar.de";

			var message = new Message
			{
				Body = new Body
				{
					HtmlContent = htmlContent,
					TextContent = textContent
				},
				Recipients = new[]
				{
					new Recipient
					{
						Email = recipient1EmailCc1,
						Name = recipient1EmailCc1.Split('@')[0],
						IsBcc = false,
						IsCc = true,
						IsReplyTo = false
					},
					new Recipient
					{
						Email = recipient2EmailCc2,
						Name = recipient2EmailCc2.Split('@')[0],
						IsBcc = false,
						IsCc = true,
						IsReplyTo = false
					},
					new Recipient
					{
						Email = recipient3EmailBcc1,
						Name = recipient3EmailBcc1.Split('@')[0],
						IsBcc = true,
						IsCc = false,
						IsReplyTo = false
					},
					new Recipient
					{
						Email = recipient4EmailBcc2,
						Name = recipient4EmailBcc2.Split('@')[0],
						IsBcc = true,
						IsCc = false,
						IsReplyTo = false
					},
					new Recipient
					{
						Email = recipient5EmailReplyTo1,
						Name = recipient5EmailReplyTo1.Split('@')[0],
						IsBcc = false,
						IsCc = false,
						IsReplyTo = true
					},
					new Recipient
					{
						Email = recipient6EmailReplyTo2,
						Name = recipient6EmailReplyTo2.Split('@')[0],
						IsBcc = false,
						IsCc = false,
						IsReplyTo = true
					},
					new Recipient
					{
						Email = recipient7EmailTo1,
						Name = recipient7EmailTo1.Split('@')[0],
						IsBcc = false,
						IsCc = false,
						IsReplyTo = false
					},
					new Recipient
					{
						Email = recipient8EmailTo2,
						Name = recipient8EmailTo2.Split('@')[0],
						IsBcc = false,
						IsCc = false,
						IsReplyTo = false
					}
				},
				Sender = new Sender
				{
					Email = sender,
					Name = sender.Split('@')[0]
				},
				Subject = subject
			};

			var mimeMessage = new MessageConverter().ToMimeMessage(message);
			Assert.NotNull(mimeMessage);

			// from
			Assert.Single(mimeMessage.From);
			Assert.Single(mimeMessage.From.Mailboxes);
			Assert.Equal(sender, mimeMessage.From.Mailboxes.Single().Address);
			Assert.Equal(sender.Split('@')[0], mimeMessage.From.Mailboxes.Single().Name);

			// cc 
			Assert.Equal(2, mimeMessage.Cc.Mailboxes.Count());
			Assert.Contains(
				mimeMessage.Cc.Mailboxes,
				x => x.Address == recipient1EmailCc1 && x.Name == recipient1EmailCc1.Split('@')[0]);
			Assert.Contains(
				mimeMessage.Cc.Mailboxes,
				x => x.Address == recipient2EmailCc2 && x.Name == recipient2EmailCc2.Split('@')[0]);

			// bcc 
			Assert.Equal(2, mimeMessage.Bcc.Mailboxes.Count());
			Assert.Contains(
				mimeMessage.Bcc.Mailboxes,
				x => x.Address == recipient3EmailBcc1 && x.Name == recipient3EmailBcc1.Split('@')[0]);
			Assert.Contains(
				mimeMessage.Bcc.Mailboxes,
				x => x.Address == recipient4EmailBcc2 && x.Name == recipient4EmailBcc2.Split('@')[0]);

			// reply to
			Assert.Equal(2, mimeMessage.ReplyTo.Mailboxes.Count());
			Assert.Contains(
				mimeMessage.ReplyTo.Mailboxes,
				x => x.Address == recipient5EmailReplyTo1 && x.Name == recipient5EmailReplyTo1.Split('@')[0]);
			Assert.Contains(
				mimeMessage.ReplyTo.Mailboxes,
				x => x.Address == recipient6EmailReplyTo2 && x.Name == recipient6EmailReplyTo2.Split('@')[0]);

			// to
			Assert.Equal(2, mimeMessage.To.Mailboxes.Count());
			Assert.Contains(
				mimeMessage.To.Mailboxes,
				x => x.Address == recipient7EmailTo1 && x.Name == recipient7EmailTo1.Split('@')[0]);
			Assert.Contains(
				mimeMessage.To.Mailboxes,
				x => x.Address == recipient8EmailTo2 && x.Name == recipient8EmailTo2.Split('@')[0]);

			// body and subject
			Assert.Equal(htmlContent, mimeMessage.HtmlBody);
			Assert.Equal(textContent, mimeMessage.TextBody);
			Assert.Equal(subject, mimeMessage.Subject);
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfNull()
		{
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(null));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfRecipientHasNoEmail()
		{
			var message = Init();
			message.Recipients.First().Email = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfRecipientHasNoName()
		{
			var message = Init();
			message.Recipients.First().Name = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfRecipientsHasOnlyReplyTo()
		{
			var message = Init();
			message.Recipients = new[]
			{
				new Recipient
				{
					Email = "foo@bar.de",
					Name = "name",
					IsReplyTo = true
				}
			};
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfRecipientsIsNull()
		{
			var message = Init();
			message.Recipients = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
			message.Recipients = Enumerable.Empty<Recipient>();
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfSenderEmailIsNull()
		{
			var message = Init();
			message.Sender.Email = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfSenderIsNull()
		{
			var message = Init();
			message.Sender = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfSenderNameIsNull()
		{
			var message = Init();
			message.Sender.Name = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		[Fact]
		public void ToMimeMessageThrowsArgumentNullExceptionIfSubjectIsNull()
		{
			var message = Init();
			message.Subject = null;
			Assert.Throws<ArgumentNullException>(() => new MessageConverter().ToMimeMessage(message));
		}

		private static Message Init()
		{
			return new Message
			{
				Sender = new Sender
				{
					Email = "foo@bar.de",
					Name = "foo",
					Smtp = new Smtp
					{
						Password = "password",
						Port = 1,
						Server = "localhost"
					}
				},
				Body = new Body
				{
					HtmlContent = "<h1>cool</h1>",
					TextContent = "cool!"
				},
				Recipients = new[]
				{
					new Recipient
					{
						Email = "to@bar.de",
						Name = "cool io"
					}
				},
				Subject = "cool io subject"
			};
		}
	}
}
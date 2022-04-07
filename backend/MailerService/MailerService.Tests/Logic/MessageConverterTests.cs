namespace MailerService.Test.Logic
{
    using System;
    using MailerService.Logic;
    using MimeKit;
    using Surveys.Common.Messages;
    using Xunit;

    public class MessageConverterTests
    {
        [Theory]
        [InlineData(
            "recipientEmail@bar.example",
            "recipientName",
            "replyToName",
            "replyToEmail@foo.example",
            "subject",
            "textHtml",
            "textPlain",
            "fromName",
            "fromEmail@foobar.example")]
        public void ToMimeMessage(
            string recipientEmail,
            string recipientName,
            string replyToName,
            string replyToEmail,
            string subject,
            string textHtml,
            string textPlain,
            string fromName,
            string fromEmail
        )
        {
            var message = new MessageConverter().ToMimeMessage(
                new SendMailMessage(
                    Guid.NewGuid().ToString(),
                    new[] {new Recipient(recipientEmail, recipientName)},
                    new Recipient(replyToEmail, replyToName),
                    subject,
                    new Body(textHtml, textPlain)),
                new[] {new MailboxAddress(fromName, fromEmail)});

            Assert.Single(message.To.Mailboxes);
            Assert.Contains(message.To.Mailboxes, mb => mb.Address == recipientEmail && mb.Name == recipientName);
            Assert.Equal(subject, message.Subject);
            Assert.Single(message.From.Mailboxes);
            Assert.Contains(message.From.Mailboxes, mb => mb.Address == fromEmail && mb.Name == fromName);
        }
    }
}

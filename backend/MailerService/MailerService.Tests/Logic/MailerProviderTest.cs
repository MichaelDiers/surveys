namespace MailerService.Tests.Logic
{
    using System;
    using System.Linq;
    using Google.Cloud.Functions.Testing;
    using MailerService.Logic;
    using MailerService.Model;
    using MailerService.Tests.Mocks;
    using Md.Common.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Messages;
    using Xunit;

    public class MailerProviderTest
    {
        [Fact]
        public async void SendAsyncDeserializeObjectSucceeds()
        {
            var sendMailMessage = new SendMailMessage(
                Guid.NewGuid().ToString(),
                new[] {new Recipient("example@example.example", "example")},
                new Recipient("reply@example.example", "reply"),
                "subject",
                new Body("<html></html>", "plain"),
                Enumerable.Empty<Attachment>());
            var json = Serializer.SerializeObject(sendMailMessage);
            var logger = new MemoryLogger<Function>();
            await new MailerProvider(
                logger,
                new MessageConverterMock(),
                new MailerSmtpClientMock(),
                new MailerServiceConfiguration(),
                new SecretManagerMock()).HandleAsync(JsonConvert.DeserializeObject<SendMailMessage>(json));
            Assert.Empty(logger.ListLogEntries());
        }
    }
}

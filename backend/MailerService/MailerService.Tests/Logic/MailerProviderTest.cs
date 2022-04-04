namespace MailerService.Test.Logic
{
    using Google.Cloud.Functions.Testing;
    using MailerService.Logic;
    using MailerService.Model;
    using MailerService.Test.Mocks;
    using Newtonsoft.Json;
    using Surveys.Common.Messages;
    using Xunit;

    public class MailerProviderTest
    {
        [Theory]
        [InlineData(
            "{\"processId\":\"53C67E0C-3B7D-417A-AF2A-FAD74684C4E7\",\"recipients\":[{\"email\":\"foo@bar.example\",\"name\":\"RecipientName\"}],\"replyTo\":{\"email\":\"foobar@bar.example\",\"name\":\"ReplyToName\"},\"subject\":\"subject\",\"text\":{\"html\":\"html body\",\"plain\":\"plain body\"},\"surveyId\":\"53C67E0C-3B7D-417A-AF2A-FAD74684C4E7\",\"participantIds\":[\"53C67E0C-3B7D-417A-AF2A-FAD74684C4E7\"],\"statusOk\":\"CREATED\",\"statusFailed\":\"CREATED\"}")]
        public async void SendAsyncDeserializeObjectSucceeds(string json)
        {
            var logger = new MemoryLogger<Function>();
            await new MailerProvider(
                logger,
                new MessageConverterMock(),
                new MailerSmtpClientMock(),
                new MailerServiceConfiguration(),
                new PubSubMock(),
                new SecretManagerMock()).HandleAsync(JsonConvert.DeserializeObject<SendMailMessage>(json));
            Assert.Empty(logger.ListLogEntries());
        }
    }
}

namespace Surveys.Common.Tests.Messages
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Messages;
    using Xunit;

    public class SendMailMessageTests
    {
        [Fact]
        public void Deserialize()
        {
            const string json =
                "{\"processId\":\"1fb37955-603e-4a91-844b-94a9b26c63e3\",\"recipients\":[{\"email\":\"foo@bar.example\",\"name\":\"foo\"}],\"replyTo\":{\"email\":\"bar@foo.example\",\"name\":\"bar\"},\"subject\":\"subject\",\"text\":{\"html\":\"html\",\"plain\":\"plain\"},\"surveyId\":\"ef6bca83-2968-45f1-a973-00c0097cdf1a\",\"participantIds\":[\"a3b8d7cd-5801-4f6d-818c-4d0d358ae709\"],\"statusOk\":\"CREATED\",\"statusFailed\":\"INVITATION_MAIL_FAILED\"}";
            var message = JsonConvert.DeserializeObject<SendMailMessage>(json);
            Assert.NotNull(message);
        }

        [Fact]
        public void Serialize()
        {
            var message = new SendMailMessage(
                Guid.NewGuid().ToString(),
                new[] {new Recipient("foo@bar.example", "foo")},
                new Recipient("bar@foo.example", "bar"),
                "subject",
                new Body("html", "plain"));
            var json = JsonConvert.SerializeObject(message);
            Assert.NotNull(json);
        }
    }
}

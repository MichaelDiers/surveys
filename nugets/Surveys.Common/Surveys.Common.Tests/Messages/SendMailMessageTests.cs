namespace Surveys.Common.Tests.Messages
{
    using System;
    using System.Linq;
    using Md.Common.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Messages;
    using Xunit;

    public class SendMailMessageTests
    {
        [Fact]
        public void Serialize()
        {
            var message = new SendMailMessage(
                Guid.NewGuid().ToString(),
                new[] {new Recipient("foo@bar.example", "foo")},
                new Recipient("bar@foo.example", "bar"),
                "subject",
                new Body("html", "plain"),
                Enumerable.Empty<Attachment>());
            var json = JsonConvert.SerializeObject(message);
            Assert.NotNull(json);
        }

        [Fact]
        public void SerializeDeserialize()
        {
            var expected = new SendMailMessage(
                Guid.NewGuid().ToString(),
                new[] {new Recipient("foobar@example.example", "foobar")},
                new Recipient("reply@example.example", "reply"),
                "subject",
                new Body("<html></html>", "plain"),
                new[] {new Attachment("name", new byte[10])});
            var actual = Serializer.DeserializeObject<SendMailMessage>(Serializer.SerializeObject(expected));

            Assert.Single(expected.Attachments);
            Assert.Single(actual.Attachments);
            Assert.Equal(expected.Attachments.First().Data, actual.Attachments.First().Data);
            Assert.Equal(expected.Attachments.First().Name, actual.Attachments.First().Name);
            Assert.Equal(expected.Body.Html, actual.Body.Html);
            Assert.Equal(expected.Body.Plain, actual.Body.Plain);
            Assert.Single(expected.Recipients);
            Assert.Single(actual.Recipients);
            Assert.Equal(expected.Recipients.First().Name, actual.Recipients.First().Name);
            Assert.Equal(expected.Recipients.First().Email, actual.Recipients.First().Email);
            Assert.Equal(expected.ReplyTo.Email, actual.ReplyTo.Email);
            Assert.Equal(expected.ReplyTo.Name, actual.ReplyTo.Name);
            Assert.Equal(expected.Subject, actual.Subject);
        }
    }
}

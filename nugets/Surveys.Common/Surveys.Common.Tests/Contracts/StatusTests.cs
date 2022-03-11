namespace Surveys.Common.Tests.Contracts
{
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Xunit;

    public class StatusTests
    {
        [Theory]
        [InlineData(Status.Created, "CREATED")]
        [InlineData(Status.InvitationMailSentFailed, "INVITATION_MAIL_FAILED")]
        [InlineData(Status.InvitationMailSentOk, "INVITATION_MAIL_OK")]
        public void Deserialize(Status status, string serialized)
        {
            var json = $"{{\"status\":\"{serialized}\"}}";
            var obj = JsonConvert.DeserializeObject<StatusTestsHelper>(json);
            Assert.NotNull(obj);
            Assert.Equal(status, obj.Status);
        }

        [Theory]
        [InlineData(Status.Created, "CREATED")]
        [InlineData(Status.InvitationMailSentFailed, "INVITATION_MAIL_FAILED")]
        [InlineData(Status.InvitationMailSentOk, "INVITATION_MAIL_OK")]
        public void Serialize(Status status, string serialized)
        {
            var obj = new StatusTestsHelper(status);
            var json = JsonConvert.SerializeObject(obj);
            Assert.Equal($"{{\"status\":\"{serialized}\"}}", json);
        }
    }
}

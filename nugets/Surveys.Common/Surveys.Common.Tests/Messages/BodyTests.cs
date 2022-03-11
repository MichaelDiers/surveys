namespace Surveys.Common.Tests.Messages
{
    using Newtonsoft.Json;
    using Surveys.Common.Messages;
    using Xunit;

    public class BodyTests
    {
        [Fact]
        public void Deserialize()
        {
            var json = "{\"html\":\"html\",\"plain\":\"plain\"}";
            var body = JsonConvert.DeserializeObject<Body>(json);
            Assert.Equal("html", body.Html);
            Assert.Equal("plain", body.Plain);
        }

        [Fact]
        public void Serialize()
        {
            var body = new Body("html", "plain");
            var json = JsonConvert.SerializeObject(body);
            Assert.Equal("{\"html\":\"html\",\"plain\":\"plain\"}", json);
        }
    }
}

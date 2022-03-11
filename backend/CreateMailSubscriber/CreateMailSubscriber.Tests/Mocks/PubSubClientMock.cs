namespace CreateMailSubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Newtonsoft.Json;
    using Xunit;

    internal class PubSubClientMock : IPubSubClient
    {
        public Task PublishAsync<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            Assert.NotNull(json);
            return Task.CompletedTask;
        }
    }
}

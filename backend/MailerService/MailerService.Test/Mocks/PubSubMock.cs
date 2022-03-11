namespace MailerService.Test.Mocks
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    public class PubSubMock : IPubSubClient
    {
        public Task PublishAsync<T>(T message)
        {
            return Task.CompletedTask;
        }
    }
}

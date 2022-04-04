namespace MailerService.Test.Mocks
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    public class SecretManagerMock : ISecretManager
    {
        public Task<string> GetStringAsync(string key)
        {
            return Task.FromResult(key);
        }
    }
}

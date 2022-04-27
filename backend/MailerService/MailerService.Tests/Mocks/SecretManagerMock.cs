namespace MailerService.Tests.Mocks
{
    using System.Threading.Tasks;
    using Md.GoogleCloudSecrets.Contracts.Logic;

    public class SecretManagerMock : ISecretManager
    {
        public Task<string> GetStringAsync(string key)
        {
            return Task.FromResult(key);
        }
    }
}

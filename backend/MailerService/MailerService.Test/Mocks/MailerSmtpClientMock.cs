namespace MailerService.Test.Mocks
{
    using System.Threading.Tasks;
    using MailerService.Contracts;
    using MimeKit;

    public class MailerSmtpClientMock : IMailerSmtpClient
    {
        public Task SendAsync(MimeMessage message, ISmtp smtp)
        {
            return Task.CompletedTask;
        }
    }
}

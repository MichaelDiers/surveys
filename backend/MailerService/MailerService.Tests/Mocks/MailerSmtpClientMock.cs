namespace MailerService.Tests.Mocks
{
    using System.Threading.Tasks;
    using MailerService.Contracts;
    using MimeKit;

    public class MailerSmtpClientMock : IMailerSmtpClient
    {
        public Task SendAsync(
            MimeMessage message,
            ISmtp smtp,
            string userName,
            string password
        )
        {
            return Task.CompletedTask;
        }
    }
}

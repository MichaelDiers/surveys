namespace MailerService.Logic
{
	using System.Threading.Tasks;
	using MailerService.Contracts;
	using MailKit.Net.Smtp;
	using MailKit.Security;
	using MimeKit;

	/// <summary>
	///   Send mails by <see cref="SmtpClient" />.
	/// </summary>
	public class MailerSmtpClient : IMailerSmtpClient
	{
		/// <summary>
		///   Sends an email message.
		/// </summary>
		/// <param name="message">The message to be send.</param>
		/// <param name="smtp">The smtp connection data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task SendAsync(MimeMessage message, ISmtp smtp)
		{
			using var client = new SmtpClient();
			await client.ConnectAsync(smtp.Server, smtp.Port, SecureSocketOptions.SslOnConnect);
			await client.AuthenticateAsync(smtp.UserName, smtp.Password);
			await client.SendAsync(message);
		}
	}
}
namespace MailerService.Logic
{
	using System;
	using System.Threading.Tasks;
	using MailerService.Contracts;
	using MailerService.Model;
	using MailKit.Net.Smtp;
	using MailKit.Security;
	using MimeKit;

	/// <summary>
	///   Provider for sending email messages.
	/// </summary>
	public class MailerProvider : IMailerProvider
	{
		/// <summary>
		///   Converter for <see cref="Message" /> to <see cref="MimeMessage" />.
		/// </summary>
		private readonly IMessageConverter messageConverter;

		/// <summary>
		///   Creates a new instance of <see cref="MailerProvider" />.
		/// </summary>
		/// <param name="messageConverter">Converter for <see cref="Message" /> to <see cref="MimeMessage" />.</param>
		public MailerProvider(IMessageConverter messageConverter)
		{
			this.messageConverter = messageConverter ?? throw new ArgumentNullException(nameof(messageConverter));
		}

		/// <summary>
		///   Sends an email message.
		/// </summary>
		/// <param name="message">The email message data.</param>
		public async Task SendAsync(Message message)
		{
			var email = this.messageConverter.ToMimeMessage(message);

			using var client = new SmtpClient();
			await client.ConnectAsync(message.Sender.Smtp.Server, message.Sender.Smtp.Port, SecureSocketOptions.SslOnConnect);
			await client.AuthenticateAsync(message.Sender.Email, message.Sender.Smtp.Password);
			await client.SendAsync(email);
		}
	}
}
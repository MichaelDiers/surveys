namespace MailerService.Contracts
{
	using System.Threading.Tasks;
	using MailerService.Model;

	/// <summary>
	///   Provider for sending emails.
	/// </summary>
	public interface IMailerProvider
	{
		/// <summary>
		///   Sends an email message.
		/// </summary>
		/// <param name="message">The email message data.</param>
		public Task SendAsync(Message message);
	}
}
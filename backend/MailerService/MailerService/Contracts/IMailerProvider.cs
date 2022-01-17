namespace MailerService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provider for sending emails.
	/// </summary>
	public interface IMailerProvider
	{
		/// <summary>
		///   Sends an email message.
		/// </summary>
		/// <param name="json">An <see cref="IMessage" /> serialized as json.</param>
		Task SendAsync(string json);
	}
}
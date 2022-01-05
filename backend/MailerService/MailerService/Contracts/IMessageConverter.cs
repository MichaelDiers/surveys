namespace MailerService.Contracts
{
	using MailerService.Model;
	using MimeKit;

	/// <summary>
	///   Converter for <see cref="Message" /> instances.
	/// </summary>
	/// .
	public interface IMessageConverter
	{
		/// <summary>
		///   Convert a <see cref="Message" /> to a <see cref="MimeMessage" />.
		/// </summary>
		/// <param name="message">The data that is used to create the <see cref="MimeMessage" />.</param>
		/// <returns>An instance of <see cref="MimeMessage" />.</returns>
		MimeMessage ToMimeMessage(Message message);
	}
}
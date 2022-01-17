namespace MailerService.Contracts
{
	using System.Collections.Generic;
	using MimeKit;

	/// <summary>
	///   Converter for <see cref="IMessage" /> to <see cref="MimeMessage" />.
	/// </summary>
	/// .
	public interface IMessageConverter
	{
		/// <summary>
		///   Convert a <see cref="IMessage" /> to a <see cref="MimeMessage" />.
		/// </summary>
		/// <param name="message">The data that is used to create the <see cref="MimeMessage" />.</param>
		/// <param name="from">Specifies the sender data.</param>
		/// <returns>An instance of <see cref="MimeMessage" />.</returns>
		MimeMessage ToMimeMessage(IMessage message, IEnumerable<InternetAddress> from);
	}
}
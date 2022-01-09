﻿namespace MailerService.Contracts
{
	using System.Collections.Generic;
	using MimeKit;

	/// <summary>
	///   Converter for <see cref="IMailerServiceRequest" /> to <see cref="MimeMessage" />.
	/// </summary>
	/// .
	public interface IMessageConverter
	{
		/// <summary>
		///   Convert a <see cref="IMailerServiceRequest" /> to a <see cref="MimeMessage" />.
		/// </summary>
		/// <param name="message">The data that is used to create the <see cref="MimeMessage" />.</param>
		/// <param name="from">Specifies the sender data.</param>
		/// <returns>An instance of <see cref="MimeMessage" />.</returns>
		MimeMessage ToMimeMessage(IMailerServiceRequest message, IEnumerable<InternetAddress> from);
	}
}
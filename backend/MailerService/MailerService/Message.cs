namespace MailerService
{
	using System.Collections.Generic;

	/// <summary>
	///   Specifies email message data.
	/// </summary>
	public class Message
	{
		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		public IEnumerable<Recipient> Recipients { get; set; }
	}
}
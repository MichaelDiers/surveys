namespace MailerService.Model
{
	using System.Collections.Generic;

	/// <summary>
	///   Specifies email message data.
	/// </summary>
	public class Message
	{
		/// <summary>
		///   Gets or sets the body of the email.
		/// </summary>
		public Body Body { get; set; }

		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		public IEnumerable<Recipient> Recipients { get; set; }

		/// <summary>
		///   Gets or sets the sender of the email.
		/// </summary>
		public Sender Sender { get; set; }

		/// <summary>
		///   Gets or sets the subject of the email.
		/// </summary>
		public string Subject { get; set; }
	}
}
namespace MailerService.Model
{
	/// <summary>
	///   Specifies an email recipient.
	/// </summary>
	public class Recipient
	{
		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets a value indicating if the recipient is set at bcc.
		/// </summary>
		public bool IsBcc { get; set; }

		/// <summary>
		///   Gets or sets a value indicating if the recipient is set at cc.
		/// </summary>
		public bool IsCc { get; set; }

		/// <summary>
		///   Gets or sets a value indicating if the recipient is specifies the reply to address.
		/// </summary>
		public bool IsReplyTo { get; set; }

		/// <summary>
		///   Gets or sets the name of the recipient.
		/// </summary>
		public string Name { get; set; }
	}
}
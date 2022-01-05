namespace MailerService.Model
{
	/// <summary>
	///   Specification of an email sender.
	/// </summary>
	public class Sender
	{
		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the recipient.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///   Gets or sets the smtp connection data.
		/// </summary>
		public Smtp Smtp { get; set; }
	}
}
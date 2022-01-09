namespace MailerService.Model
{
	/// <summary>
	///   Specifies the sender of emails.
	/// </summary>
	public class MailboxAddressFrom
	{
		/// <summary>
		///   Gets or sets the address of the sender.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///   Gets or sets the name of the sender.
		/// </summary>
		public string Name { get; set; }
	}
}
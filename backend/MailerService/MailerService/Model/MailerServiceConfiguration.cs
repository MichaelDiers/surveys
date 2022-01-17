namespace MailerService.Model
{
	using MailerService.Contracts;

	/// <summary>
	///   Specifies the application configuration.
	/// </summary>
	public class MailerServiceConfiguration : IMailerServiceConfiguration
	{
		/// <summary>
		///   Gets or sets the sender data of an email.
		/// </summary>
		public Recipient MailboxAddressFrom { get; set; }

		/// <summary>
		///   Gets or sets the connection data for smtp.
		/// </summary>
		public Smtp Smtp { get; set; }

		/// <summary>
		///   Gets or sets the handle for new lines in templates.
		/// </summary>
		public string TemplateNewline { get; set; }
	}
}
namespace MailerService.Contracts
{
	using MailerService.Model;

	/// <summary>
	///   Specification of the application configuration.
	/// </summary>
	public interface IMailerServiceConfiguration
	{
		/// <summary>
		///   Gets the sender data of emails.
		/// </summary>
		public Recipient MailboxAddressFrom { get; }

		/// <summary>
		///   Gets the configuration for the smtp client.
		/// </summary>
		public Smtp Smtp { get; }

		/// <summary>
		///   Gets the handle for new lines in templates.
		/// </summary>
		public string TemplateNewline { get; }
	}
}
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
	}
}
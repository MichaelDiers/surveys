namespace MailerService.Contracts
{
	using MailerService.Model;

	/// <summary>
	///   Specification of the application configuration.
	/// </summary>
	public interface IMailerServiceConfiguration
	{
		/// <summary>
		///   Gets or sets the sender data of emails.
		/// </summary>
		public MailboxAddressFrom MailboxAddressFrom { get; set; }

		/// <summary>
		///   Gets the configuration for the smtp client.
		/// </summary>
		public Smtp Smtp { get; }

		/// <summary>
		///   Gets the templates for survey request templates.
		/// </summary>
		MessageTemplate SurveyRequestTemplate { get; }
	}
}
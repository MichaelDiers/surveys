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
		public MailboxAddressFrom MailboxAddressFrom { get; set; }

		/// <summary>
		///   Gets or sets the connection data for smtp.
		/// </summary>
		public Smtp Smtp { get; set; }

		/// <summary>
		///   Gets or sets the templates for survey request emails.
		/// </summary>
		public MessageTemplate SurveyRequestTemplate { get; set; }

		/// <summary>
		///   Gets or sets the templates for thank you emails.
		/// </summary>
		public MessageTemplate ThankYouTemplate { get; set; }
	}
}
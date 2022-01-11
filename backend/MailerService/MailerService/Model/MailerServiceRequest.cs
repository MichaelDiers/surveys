namespace MailerService.Model
{
	using System.Collections.Generic;
	using MailerService.Contracts;

	/// <summary>
	///   Specifies the data of a <see cref="MailerFunction" /> request.
	/// </summary>
	public class MailerServiceRequest : IMailerServiceRequest
	{
		/// <summary>
		///   Gets the type of the requested email.
		/// </summary>
		public EmailType EmailType { get; set; }

		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		public IEnumerable<Recipient> Recipients { get; set; }

		/// <summary>
		///   Gets or sets the reply to email address.
		/// </summary>
		public Recipient ReplyTo { get; set; }

		/// <summary>
		///   Gets or sets the link to the survey.
		/// </summary>
		public string SurveyLink { get; set; }

		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		public string SurveyName { get; set; }
	}
}
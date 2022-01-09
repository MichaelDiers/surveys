namespace MailerService.Contracts
{
	using System.Collections.Generic;
	using MailerService.Model;

	/// <summary>
	///   Specifies the data of a <see cref="MailerFunction" /> request.
	/// </summary>
	public interface IMailerServiceRequest
	{
		/// <summary>
		///   Gets the type of the requested email.
		/// </summary>
		EmailType EmailType { get; }

		/// <summary>
		///   Gets the recipients of the email.
		/// </summary>
		IEnumerable<Recipient> Recipients { get; }

		/// <summary>
		///   Gets the link to the survey.
		/// </summary>
		string SurveyLink { get; }

		/// <summary>
		///   Gets the name of the survey.
		/// </summary>
		string SurveyName { get; }
	}
}
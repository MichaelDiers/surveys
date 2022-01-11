namespace MailerService.Contracts
{
	/// <summary>
	///   Specifies the types of emails that the service sends.
	/// </summary>
	public enum EmailType
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Service sends a request to participate in a survey.
		/// </summary>
		SurveyRequest = 1,

		/// <summary>
		///   Send a thank you email to a survey participant.
		/// </summary>
		ThankYou = 2
	}
}
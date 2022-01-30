namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes valid values for the status of a survey.
	/// </summary>
	public enum SurveyStatusValue
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Indicates that a survey is created.
		/// </summary>
		Created = 1,

		/// <summary>
		///   Invitation mails are sent to the participants.
		/// </summary>
		InvitationMailOk = 2,

		/// <summary>
		///   The survey is closed.
		/// </summary>
		Closed = 3,

		/// <summary>
		///   Thank you mail is sent to participants.
		/// </summary>
		ThankYouMailOk = 4,

		/// <summary>
		///   Thank you mail sending failed.
		/// </summary>
		ThankYouMailFailed = 5
	}
}
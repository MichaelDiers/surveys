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
		///   Invitation mails sending failed.
		/// </summary>
		InvitationMailFailed = 3,

		/// <summary>
		///   The survey is closed.
		/// </summary>
		Closed = 4,

		/// <summary>
		///   Thank you mail is sent to participants.
		/// </summary>
		ThankYouMailOk = 5,

		/// <summary>
		///   Thank you mail sending failed.
		/// </summary>
		ThankYouMailFailed = 6,

		/// <summary>
		///   Survey closed mail is sent to participants.
		/// </summary>
		SurveyClosedMailOk = 7,

		/// <summary>
		///   Survey closed mail sending failed.
		/// </summary>
		SurveyClosedMailFailed = 8
	}
}
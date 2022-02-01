namespace UpdateSurveyStatusService.Contracts
{
	/// <summary>
	///   Specifies status value for surveys.
	/// </summary>
	public enum Status
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Survey is created.
		/// </summary>
		Created = 1,

		/// <summary>
		///   Indicates that sending an invitation mail failed.
		/// </summary>
		InvitationMailFailed = 2,

		/// <summary>
		///   Indicates that sending an invitation mail is sent.
		/// </summary>
		InvitationMailOk = 3,

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
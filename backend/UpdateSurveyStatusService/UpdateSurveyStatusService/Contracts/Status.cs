namespace UpdateSurveyStatusService.Contracts
{
	/// <summary>
	///   Specifies status value for surveys and participants.
	/// </summary>
	public enum Status
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Invitation mails are requested for all participants. All requested did succeed.
		/// </summary>
		InvitationMailsRequestOk = 1,

		/// <summary>
		///   Invitation mails are requested for all participants. At least one request did fail.
		/// </summary>
		InvitationMailsRequestFailed = 2,

		/// <summary>
		///   Mail is sent to a participant.
		/// </summary>
		MailSentOk = 3,

		/// <summary>
		///   Sending an email to a participant failed.
		/// </summary>
		MailSentFailed = 4
	}
}
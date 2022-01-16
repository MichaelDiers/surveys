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
		InvitationMailsRequestFailed = 2
	}
}
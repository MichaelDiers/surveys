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
		InvitationMailOk = 3
	}
}
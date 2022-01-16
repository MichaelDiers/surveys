namespace UpdateSurveyStatusService.Contracts
{
	/// <summary>
	///   Specifies the type of the survey update.
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		///   Undefined value.
		/// </summary>
		None = 0,

		/// <summary>
		///   Update the status of a survey.
		/// </summary>
		Survey = 1,

		/// <summary>
		///   Update the participant status of a survey.
		/// </summary>
		Participant = 2
	}
}
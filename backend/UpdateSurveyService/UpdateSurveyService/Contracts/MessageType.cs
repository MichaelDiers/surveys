namespace UpdateSurveyService.Contracts
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
		Status = 1
	}
}
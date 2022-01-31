namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes a survey status update request.
	/// </summary>
	public interface ISurveyStatusUpdateRequest
	{
		/// <summary>
		///   Gets the participant id.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the new status.
		/// </summary>
		SurveyStatusValue Status { get; }

		/// <summary>
		///   Gets the survey id.
		/// </summary>
		string SurveyId { get; }
	}
}
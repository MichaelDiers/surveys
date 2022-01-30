namespace SurveyEvaluatorService.Contracts
{
	using System;

	/// <summary>
	///   Describes a survey status update.
	/// </summary>
	public interface ISurveyStatus
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the status of the survey.
		/// </summary>
		SurveyStatusValue Status { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }

		/// <summary>
		///   Gets the creation data of the status.
		/// </summary>
		DateTime Timestamp { get; }
	}
}
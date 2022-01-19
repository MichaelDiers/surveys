namespace SurveyViewerService.Contracts
{
	using System;

	/// <summary>
	///   Describes status data of a survey.
	/// </summary>
	public interface ISurveyStatus
	{
		/// <summary>
		///   Gets the id of the survey participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the status of the survey.
		/// </summary>
		string Status { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }

		/// <summary>
		///   Gets the creation date and time of the status.
		/// </summary>
		DateTime TimeStamp { get; }
	}
}
namespace SurveyViewerService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
	public interface ISurveyResult
	{
		/// <summary>
		///   Gets the answers of the participant.
		/// </summary>
		IEnumerable<IAnswer> Answers { get; }

		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
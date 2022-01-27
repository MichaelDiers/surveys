namespace SurveyViewerService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes the survey result of a survey participant.
	/// </summary>
	public interface ISurveySubmitResult
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the question ids and its results.
		/// </summary>
		IEnumerable<ISurveySubmitResultQuestion> Questions { get; }
	}
}
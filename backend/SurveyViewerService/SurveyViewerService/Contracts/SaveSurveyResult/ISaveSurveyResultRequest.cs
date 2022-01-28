namespace SurveyViewerService.Contracts.SaveSurveyResult
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes the results of a survey for a participant.
	/// </summary>
	public interface ISaveSurveyResultRequest
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the survey results.
		/// </summary>
		IEnumerable<ISurveyResult> Results { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
namespace SurveyEvaluatorService.Contracts
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
	public interface ISurveyResult
	{
		/// <summary>
		///   Gets the id of a participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the results of the survey for the participant.
		/// </summary>
		IEnumerable<ISurveyResultAnswer> Results { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }

		/// <summary>
		///   Gets the creation timestamp of the result.
		/// </summary>
		DateTime Timestamp { get; }
	}
}
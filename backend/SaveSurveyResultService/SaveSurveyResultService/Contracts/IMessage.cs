namespace SaveSurveyResultService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Specifies the incoming messages of the cloud function.
	/// </summary>
	public interface IMessage
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string ParticipantId { get; }

		/// <summary>
		///   Gets the survey results.
		/// </summary>
		IEnumerable<IResult> Results { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
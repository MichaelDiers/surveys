namespace SurveyEvaluatorService.Contracts
{
	using System.Collections.Generic;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Describes a closed survey request.
	/// </summary>
	public interface ISurveyClosedRequest
	{
		/// <summary>
		///   Gets the name of the survey.
		/// </summary>
		string Name { get; }

		/// <summary>
		///   Gets the organizer of the survey.
		/// </summary>
		SurveyClosedRequestOrganizer Organizer { get; }

		/// <summary>
		///   Gets the participants of the survey.
		/// </summary>
		IList<SurveyClosedRequestParticipant> Participants { get; }
	}
}
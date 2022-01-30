namespace SurveyEvaluatorService.Contracts
{
	using System;
	using System.Collections.Generic;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Describes the data of a survey.
	/// </summary>
	public interface ISurvey
	{
		/// <summary>
		///   Gets the id of the survey-
		/// </summary>
		string Id { get; }

		/// <summary>
		///   Gets the name of the survey.
		/// </summary>
		string Name { get; }

		/// <summary>
		///   Gets the organizer of the survey.
		/// </summary>
		SurveyOrganizer Organizer { get; }

		/// <summary>
		///   Gets the id of the survey participants.
		/// </summary>
		IEnumerable<string> ParticipantIds { get; }

		/// <summary>
		///   Gets the participants of the survey.
		/// </summary>
		IEnumerable<SurveyParticipant> Participants { get; }

		/// <summary>
		///   Gets the questions.
		/// </summary>
		IEnumerable<SurveyQuestion> Questions { get; }

		/// <summary>
		///   Gets the creation date.
		/// </summary>
		DateTime Timestamp { get; }
	}
}
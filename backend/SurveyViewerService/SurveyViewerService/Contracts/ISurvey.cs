namespace SurveyViewerService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes a survey.
	/// </summary>
	public interface ISurvey
	{
		/// <summary>
		///   Gets the name of the survey.
		/// </summary>
		string Name { get; }

		/// <summary>
		///   Gets the organizer of a survey.
		/// </summary>
		IAddress Organizer { get; }

		/// <summary>
		///   Gets the participants of the survey.
		/// </summary>
		IEnumerable<IParticipant> Participants { get; }

		/// <summary>
		///   Gets the questions of the survey.
		/// </summary>
		public IEnumerable<IQuestion> Questions { get; set; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
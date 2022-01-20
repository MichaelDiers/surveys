namespace SurveyViewerService.Model
{
	using System.Collections.Generic;

	/// <summary>
	///   Survey data that is used in frontend context.
	/// </summary>
	public class SurveyViewData
	{
		/// <summary>
		///   Gets or sets a value that indicates if the survey is closed.
		/// </summary>
		public bool IsClosed { get; set; }

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the name of the participant.
		/// </summary>
		public string ParticipantName { get; set; }

		/// <summary>
		///   Gets or sets the questions of the survey.
		/// </summary>
		public IEnumerable<SurveyViewDataQuestion> Questions { get; set; }

		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		public string SurveyName { get; set; }
	}
}
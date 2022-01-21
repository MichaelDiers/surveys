namespace SurveyViewerService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	///   Survey data that is used in frontend context.
	/// </summary>
	public class SurveyViewData
	{
		/// <summary>
		///   Gets or sets a value that indicates if the survey is closed.
		/// </summary>
		[JsonProperty("isClosed")]
		public bool IsClosed { get; set; }

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[JsonProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the name of the participant.
		/// </summary>
		[JsonProperty("participantName")]
		public string ParticipantName { get; set; }

		/// <summary>
		///   Gets or sets the questions of the survey.
		/// </summary>
		[JsonProperty("questions")]
		public IEnumerable<SurveyViewDataQuestion> Questions { get; set; }

		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		[JsonProperty("surveyName")]
		public string SurveyName { get; set; }
	}
}
namespace SurveyViewerService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes the survey result of a survey participant.
	/// </summary>
	public class SurveySubmitResult : ISurveySubmitResult
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveySubmitResult" />.
		/// </summary>
		/// <param name="questions">The questions and answers of the survey.</param>
		[JsonConstructor]
		public SurveySubmitResult(IEnumerable<SurveySubmitResultQuestion> questions)
		{
			this.Questions = questions;
		}

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[JsonProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the question ids and its results.
		/// </summary>
		[JsonProperty("questions")]
		public IEnumerable<ISurveySubmitResultQuestion> Questions { get; set; }
	}
}
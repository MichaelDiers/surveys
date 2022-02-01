namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;

	/// <summary>
	///   Describes a question and answer of a survey.
	/// </summary>
	public class SurveyClosedRequestParticipantAnswer
	{
		/// <summary>
		///   Gets or sets the answer of the question.
		/// </summary>
		[JsonProperty("answer", Order = 2)]
		public string Answer { get; set; }

		/// <summary>
		///   Gets or sets the question.
		/// </summary>
		[JsonProperty("question", Order = 1)]
		public string Question { get; set; }
	}
}
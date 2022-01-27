namespace SurveyViewerService.Model
{
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes the questions and answers of a survey result.
	/// </summary>
	public class SurveySubmitResultQuestion : ISurveySubmitResultQuestion
	{
		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("questionId")]
		public string QuestionId { get; set; }

		/// <summary>
		///   Gets or sets the value of the answer.
		/// </summary>
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}
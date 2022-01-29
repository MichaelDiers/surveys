namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes an answer of a survey question.
	/// </summary>
	public class SurveyResultAnswer : ISurveyResultAnswer
	{
		/// <summary>
		///   Gets or sets the value of the answer.
		/// </summary>
		[JsonProperty("answer", Required = Required.Always)]
		public string AnswerValue { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("questionId", Required = Required.Always)]
		public string QuestionId { get; set; }
	}
}
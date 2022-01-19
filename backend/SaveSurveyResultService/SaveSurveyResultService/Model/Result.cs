namespace SaveSurveyResultService.Model
{
	using Newtonsoft.Json;
	using SaveSurveyResultService.Contracts;

	/// <summary>
	///   Specifies the answer of a survey question.
	/// </summary>
	public class Result : IResult
	{
		/// <summary>
		///   Gets or sets the answer of a survey question.
		/// </summary>
		[JsonProperty("answer", Required = Required.Always)]
		public int Answer { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("question", Required = Required.Always)]
		public string QuestionId { get; set; }
	}
}
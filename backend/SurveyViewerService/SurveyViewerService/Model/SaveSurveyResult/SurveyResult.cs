namespace SurveyViewerService.Model.SaveSurveyResult
{
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts.SaveSurveyResult;

	/// <summary>
	///   Describes the answer of a survey question.
	/// </summary>
	public class SurveyResult : ISurveyResult
	{
		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("question", Required = Required.Always, Order = 1)]
		public string QuestionId { get; set; }

		/// <summary>
		///   Gets or sets the answer of a survey question.
		/// </summary>
		[JsonProperty("answer", Required = Required.Always, Order = 2)]
		public int QuestionValue { get; set; }
	}
}
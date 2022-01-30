namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes an email body.
	/// </summary>
	public class SendMailRequestBody : ISendMailRequestBody
	{
		/// <summary>
		///   Gets or sets the html formatted email body.
		/// </summary>
		[JsonProperty("html", Order = 1, Required = Required.Always)]
		public string Html { get; set; }

		/// <summary>
		///   Gets or sets the plain text formatted email body.
		/// </summary>
		[JsonProperty("plain", Order = 2, Required = Required.Always)]
		public string PlainText { get; set; }
	}
}
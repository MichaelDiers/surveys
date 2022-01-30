namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes an email recipient.
	/// </summary>
	public class SendMailRequestRecipient : ISendMailRequestRecipient
	{
		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		[JsonProperty("email", Order = 1, Required = Required.Always)]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the recipient.
		/// </summary>
		[JsonProperty("name", Order = 2, Required = Required.Always)]
		public string Name { get; set; }
	}
}
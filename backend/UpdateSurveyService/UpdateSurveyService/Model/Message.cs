namespace UpdateSurveyService.Model
{
	using Newtonsoft.Json;
	using UpdateSurveyService.Contracts;

	/// <summary>
	///   Specifies the incoming messages of cloud function.
	/// </summary>
	public class Message
	{
		/// <summary>
		///   Gets or sets the new status of a survey.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets or sets the type of the update that is processed.
		/// </summary>
		[JsonProperty("type")]
		public MessageType Type { get; set; }
	}
}
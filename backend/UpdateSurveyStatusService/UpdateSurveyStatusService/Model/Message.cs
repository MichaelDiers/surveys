namespace UpdateSurveyStatusService.Model
{
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Contracts;
	using UpdateSurveyStatusService.Logic;

	/// <summary>
	///   Specifies the incoming messages of cloud function.
	/// </summary>
	public class Message
	{
		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[JsonProperty("participantId", Required = Required.AllowNull)]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the new status.
		/// </summary>
		[JsonProperty("status", Required = Required.Always)]
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always)]
		public string SurveyId { get; set; }
	}
}
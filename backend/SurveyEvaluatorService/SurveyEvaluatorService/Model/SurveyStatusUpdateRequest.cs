namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Converter;

	/// <summary>
	///   Describes a survey status update request.
	/// </summary>
	public class SurveyStatusUpdateRequest : ISurveyStatusUpdateRequest
	{
		/// <summary>
		///   Gets or sets the participant id.
		/// </summary>
		[JsonProperty("participantId", Required = Required.Always, Order = 2)]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or set the new status.
		/// </summary>
		[JsonProperty("status", Required = Required.Always, Order = 3)]
		[JsonConverter(typeof(SurveyStatusValueConverter))]
		public SurveyStatusValue Status { get; set; }

		/// <summary>
		///   Gets or sets the survey id.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always, Order = 1)]
		public string SurveyId { get; set; }
	}
}
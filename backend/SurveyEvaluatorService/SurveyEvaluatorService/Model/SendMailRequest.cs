namespace SurveyEvaluatorService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Converter;

	/// <summary>
	///   Describes a send mail request to google Pub/Sub.
	/// </summary>
	public class SendMailRequest : ISendMailRequest
	{
		/// <summary>
		///   Gets or sets the email body.
		/// </summary>
		[JsonProperty("text", Order = 4, Required = Required.Always)]
		public ISendMailRequestBody Body { get; set; }

		/// <summary>
		///   Gets or sets the organizer of the survey.
		/// </summary>
		[JsonProperty("replyTo", Order = 2, Required = Required.Always)]
		public ISendMailRequestRecipient Organizer { get; set; }

		/// <summary>
		///   Gets or sets the ids of the participants.
		/// </summary>
		[JsonProperty("participantIds", Order = 6, Required = Required.Always)]
		public IEnumerable<string> ParticipantIds { get; set; }

		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		[JsonProperty("recipients", Order = 1, Required = Required.Always)]
		public IEnumerable<ISendMailRequestRecipient> Recipients { get; set; }

		/// <summary>
		///   Gets or sets the status that indicates the email is sent.
		/// </summary>
		[JsonProperty(
			"statusFailed",
			Order = 8,
			Required = Required.Always)]
		[JsonConverter(typeof(SurveyStatusValueConverter))]
		public SurveyStatusValue StatusFailed { get; set; }

		/// <summary>
		///   Gets or sets the status that indicates the email is sent.
		/// </summary>
		[JsonProperty(
			"statusOk",
			Order = 7,
			Required = Required.Always)]
		[JsonConverter(typeof(SurveyStatusValueConverter))]
		public SurveyStatusValue StatusOk { get; set; }

		/// <summary>
		///   Gets or sets the subject of the email.
		/// </summary>
		[JsonProperty("subject", Order = 3, Required = Required.Always)]
		public string Subject { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Order = 5, Required = Required.Always)]
		public string SurveyId { get; set; }
	}
}
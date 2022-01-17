namespace MailerService.Model
{
	using System.Collections.Generic;
	using MailerService.Contracts;
	using Newtonsoft.Json;

	/// <summary>
	///   Specifies the data of a <see cref="MailerFunction" /> request.
	/// </summary>
	public class Message : IMessage
	{
		/// <summary>
		///   Gets or ses the body of the message.
		/// </summary>
		[JsonProperty("text", Required = Required.Always)]
		public Body Body { get; set; }

		/// <summary>
		///   Gets or sets the id of the recipients or participants.
		/// </summary>
		[JsonProperty("participantIds", Required = Required.Always)]
		public IEnumerable<string> ParticipantIds { get; set; }

		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		[JsonProperty("recipients", Required = Required.Always)]
		public IEnumerable<Recipient> Recipients { get; set; }

		/// <summary>
		///   Gets or sets the reply to email address.
		/// </summary>
		[JsonProperty("replyTo", Required = Required.Always)]
		public Recipient ReplyTo { get; set; }

		/// <summary>
		///   Gets or sets the status that indicates failure.
		/// </summary>
		[JsonProperty("statusFailed", Required = Required.Always)]
		public string StatusFailed { get; set; }

		/// <summary>
		///   Gets or sets the status that indicates success.
		/// </summary>
		[JsonProperty("statusOk", Required = Required.Always)]
		public string StatusOk { get; set; }

		/// <summary>
		///   Gets or sets the subject of the email.
		/// </summary>
		[JsonProperty("subject", Required = Required.Always)]
		public string Subject { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always)]
		public string SurveyId { get; set; }
	}
}
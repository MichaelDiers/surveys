namespace UpdateSurveyStatusService.Model
{
	using System;
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Contracts;
	using UpdateSurveyStatusService.Logic;

	/// <summary>
	///   Specifies the incoming messages of cloud function.
	/// </summary>
	public class Message
	{
		/// <summary>
		///   Gets or sets the id of the survey or the participant.
		/// </summary>
		[JsonProperty("id", Required = Required.DisallowNull)]
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the new status of a survey or a participant.
		/// </summary>
		[JsonProperty("status", Required = Required.DisallowNull)]
		[JsonConverter(typeof(StatusJsonConverter))]
		public Status Status { get; set; }

		/// <summary>
		///   Gets or sets the type of the update that is processed.
		/// </summary>
		[JsonProperty("type", Required = Required.DisallowNull)]
		[JsonConverter(typeof(MessageTypeJsonConverter))]
		public MessageType Type { get; set; }

		/// <summary>
		///   Check if the <see cref="Status" /> and <see cref="MessageType" /> are a valid combination.
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			switch (this.Type)
			{
				case MessageType.Survey:
					return this.Status == Status.InvitationMailsRequestFailed || this.Status == Status.InvitationMailsRequestOk;
				case MessageType.Participant:
					return this.Status == Status.MailSentFailed || this.Status == Status.MailSentOk;
				default:
					throw new ArgumentOutOfRangeException(nameof(this.Type), this.Type, null);
			}
		}
	}
}
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
		[JsonProperty("text", Required = Required.DisallowNull)]
		public Body Body { get; set; }

		/// <summary>
		///   Gets or sets the id of the recipients or participants.
		/// </summary>
		[JsonProperty("participantIds", Required = Required.DisallowNull)]
		public IEnumerable<string> ParticipantIds { get; set; }

		/// <summary>
		///   Gets or sets the recipients of the email.
		/// </summary>
		[JsonProperty("recipients", Required = Required.DisallowNull)]
		public IEnumerable<Recipient> Recipients { get; set; }

		/// <summary>
		///   Gets or sets the reply to email address.
		/// </summary>
		[JsonProperty("replyTo", Required = Required.DisallowNull)]
		public Recipient ReplyTo { get; set; }

		/// <summary>
		///   Gets or sets the subject of the email.
		/// </summary>
		[JsonProperty("subject", Required = Required.DisallowNull)]
		public string Subject { get; set; }
	}
}
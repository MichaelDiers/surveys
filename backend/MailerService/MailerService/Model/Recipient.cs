namespace MailerService.Model
{
	using MailerService.Contracts;
	using Newtonsoft.Json;

	/// <summary>
	///   Specifies an email recipient.
	/// </summary>
	public class Recipient : IRecipient
	{
		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		[JsonProperty("email")]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the recipient.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
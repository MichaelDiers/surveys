namespace MailerService.Model
{
	using Newtonsoft.Json;

	/// <summary>
	///   Specifies the body data of an email.
	/// </summary>
	public class Body
	{
		/// <summary>
		///   Gets or sets the html content.
		/// </summary>
		[JsonProperty("html", Required = Required.Always)]
		public string Html { get; set; }

		/// <summary>
		///   Gets or sets the plain text content.
		/// </summary>
		[JsonProperty("plain", Required = Required.Always)]
		public string Plain { get; set; }
	}
}
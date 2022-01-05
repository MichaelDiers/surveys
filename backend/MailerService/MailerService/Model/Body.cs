namespace MailerService.Model
{
	/// <summary>
	///   Specifies the body of an email.
	/// </summary>
	public class Body
	{
		/// <summary>
		///   Gets or sets the html content of the body.
		/// </summary>
		public string HtmlContent { get; set; }

		/// <summary>
		///   Gets or sets the text content of the body.
		/// </summary>
		public string TextContent { get; set; }
	}
}
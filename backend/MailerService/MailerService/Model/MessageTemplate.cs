namespace MailerService.Model
{
	/// <summary>
	///   Defines text and html templates for survey emails.
	/// </summary>
	public class MessageTemplate
	{
		/// <summary>
		///   Gets or sets the html template.
		/// </summary>
		public string Html { get; set; }

		/// <summary>
		///   Gets or sets the text template.
		/// </summary>
		public string Text { get; set; }
	}
}
namespace MailerService.Model
{
	/// <summary>
	///   Specifies the smtp connection data.
	/// </summary>
	public class Smtp
	{
		/// <summary>
		///   Gets or sets the smtp password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///   Gets or sets the smtp port.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		///   Gets or sets the smtp server.
		/// </summary>
		public string Server { get; set; }
	}
}
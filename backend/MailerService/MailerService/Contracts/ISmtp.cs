namespace MailerService.Contracts
{
	/// <summary>
	///   Specifies the smtp connection data.
	/// </summary>
	public interface ISmtp
	{
		/// <summary>
		///   Gets the smtp password.
		/// </summary>
		string Password { get; }

		/// <summary>
		///   Gets the smtp port.
		/// </summary>
		int Port { get; }

		/// <summary>
		///   Gets the smtp server.
		/// </summary>
		string Server { get; }

		/// <summary>
		///   Gets the name of the user.
		/// </summary>
		string UserName { get; }
	}
}
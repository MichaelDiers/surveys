namespace MailerService.Contracts
{
	/// <summary>
	///   Specifies an email recipient.
	/// </summary>
	public interface IRecipient
	{
		/// <summary>
		///   Gets the email address.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the name of the recipient.
		/// </summary>
		string Name { get; }
	}
}
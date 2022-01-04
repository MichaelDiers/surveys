﻿namespace MailerService
{
	/// <summary>
	///   Specifies an email recipient.
	/// </summary>
	public class Recipient
	{
		/// <summary>
		///   Gets or sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the recipient.
		/// </summary>
		public string Name { get; set; }
	}
}
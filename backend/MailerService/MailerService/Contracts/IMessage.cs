﻿namespace MailerService.Contracts
{
	using System.Collections.Generic;
	using MailerService.Model;

	/// <summary>
	///   Specifies the data of a <see cref="MailerFunction" /> request.
	/// </summary>
	public interface IMessage
	{
		/// <summary>
		///   Gets the body of the message.
		/// </summary>
		Body Body { get; }

		/// <summary>
		///   Gets the id of the recipients or participants.
		/// </summary>
		IEnumerable<string> ParticipantIds { get; }

		/// <summary>
		///   Gets the recipients of the email.
		/// </summary>
		IEnumerable<Recipient> Recipients { get; }

		/// <summary>
		///   Gets the reply to email address.
		/// </summary>
		Recipient ReplyTo { get; }

		/// <summary>
		///   Gets the status that indicates failure.
		/// </summary>
		string StatusFailed { get; }

		/// <summary>
		///   Gets the status that indicates success.
		/// </summary>
		string StatusOk { get; }

		/// <summary>
		///   Gets the subject of the email.
		/// </summary>
		string Subject { get; }

		/// <summary>
		///   Gets  the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
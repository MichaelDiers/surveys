namespace SurveyEvaluatorService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes a send mail request to google Pub/Sub.
	/// </summary>
	public interface ISendMailRequest
	{
		/// <summary>
		///   Gets the email body.
		/// </summary>
		ISendMailRequestBody Body { get; }

		/// <summary>
		///   Gets the organizer of the survey.
		/// </summary>
		ISendMailRequestRecipient Organizer { get; }

		/// <summary>
		///   Gets the ids of the participants.
		/// </summary>
		IEnumerable<string> ParticipantIds { get; }

		/// <summary>
		///   Gets the recipients of the email.
		/// </summary>
		IEnumerable<ISendMailRequestRecipient> Recipients { get; }

		/// <summary>
		///   Gets the status that indicates the email is sent.
		/// </summary>
		SurveyStatusValue StatusFailed { get; }

		/// <summary>
		///   Gets the status that indicates the email is sent.
		/// </summary>
		SurveyStatusValue StatusOk { get; }

		/// <summary>
		///   Gets the subject of the email.
		/// </summary>
		string Subject { get; }

		/// <summary>
		///   Gets the id of the survey.
		/// </summary>
		string SurveyId { get; }
	}
}
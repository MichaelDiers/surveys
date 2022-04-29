namespace Surveys.Common.Contracts.Messages
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Message for creating emails.
    /// </summary>
    public interface ICreateMailMessage : IMessage
    {
        /// <summary>
        ///     Gets a value that specifies the type of the email.
        /// </summary>
        MailType MailType { get; }

        /// <summary>
        ///     Gets the participant ids for that a reminder is sent.
        /// </summary>
        IEnumerable<string> ReminderParticipantIds { get; }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        ISurvey Survey { get; }

        /// <summary>
        ///     Gets the optional survey result data.
        /// </summary>
        ISurveyResult? SurveyResult { get; }
    }
}

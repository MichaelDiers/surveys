namespace Surveys.Common.Contracts.Messages
{
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Describes a send mail message.
    /// </summary>
    public interface ISendMailMessage : IMessage
    {
        /// <summary>
        ///     Gets or ses the body of the message.
        /// </summary>
        IBody Body { get; }

        /// <summary>
        ///     Gets or sets the id of the recipients or participants.
        /// </summary>
        IEnumerable<string> ParticipantIds { get; }

        /// <summary>
        ///     Gets or sets the recipients of the email.
        /// </summary>
        IEnumerable<IRecipient> Recipients { get; }

        /// <summary>
        ///     Gets or sets the reply to email address.
        /// </summary>
        IRecipient ReplyTo { get; }

        /// <summary>
        ///     Gets or sets the status that indicates failure.
        /// </summary>
        Status StatusFailed { get; }

        /// <summary>
        ///     Gets or sets the status that indicates success.
        /// </summary>
        Status StatusOk { get; }

        /// <summary>
        ///     Gets or sets the subject of the email.
        /// </summary>
        string Subject { get; }

        /// <summary>
        ///     Gets or sets the id of the survey.
        /// </summary>
        string SurveyId { get; }
    }
}

namespace Surveys.Common.Contracts.Messages
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Describes a send mail message.
    /// </summary>
    public interface ISendMailMessage : IMessage
    {
        IEnumerable<IAttachment> Attachments { get; }

        /// <summary>
        ///     Gets or ses the body of the message.
        /// </summary>
        Body Body { get; }

        /// <summary>
        ///     Gets or sets the recipients of the email.
        /// </summary>
        IEnumerable<IRecipient> Recipients { get; }

        /// <summary>
        ///     Gets or sets the reply to email address.
        /// </summary>
        IRecipient ReplyTo { get; }

        /// <summary>
        ///     Gets or sets the subject of the email.
        /// </summary>
        string Subject { get; }
    }
}

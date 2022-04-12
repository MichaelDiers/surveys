namespace Surveys.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Message for creating emails.
    /// </summary>
    public interface ICreateMailMessage : IMessage
    {
        /// <summary>
        ///     Gets the id of a document. The type of the document depends on the <see cref="MailType" />.
        /// </summary>
        string? DocumentId { get; }

        /// <summary>
        ///     Gets a value that specifies the type of the email.
        /// </summary>
        MailType MailType { get; }

        /// <summary>
        ///     Gets the data for <see cref="Surveys.Common.Contracts.Messages.MailType.RequestForParticipation" />
        /// </summary>
        IRequestForParticipation? RequestForParticipation { get; }
    }
}

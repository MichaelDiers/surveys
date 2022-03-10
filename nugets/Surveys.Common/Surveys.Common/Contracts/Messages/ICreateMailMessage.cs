namespace Surveys.Common.Contracts.Messages
{
    using Md.GoogleCloud.Base.Contracts.Messages;

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
        ///     Gets the data for <see cref="Surveys.Common.Contracts.Messages.MailType.RequestForParticipation" />
        /// </summary>
        IRequestForParticipation? RequestForParticipation { get; }
    }
}

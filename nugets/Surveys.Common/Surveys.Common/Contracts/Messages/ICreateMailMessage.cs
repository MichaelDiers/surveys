namespace Surveys.Common.Contracts.Messages
{
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
        ///     Gets the survey data.
        /// </summary>
        ISurvey? Survey { get; }
    }
}

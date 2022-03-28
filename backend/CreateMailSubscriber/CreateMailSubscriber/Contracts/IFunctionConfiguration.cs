namespace CreateMailSubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the url format for the survey front end.
        /// </summary>
        string FrondEndUrlFormat { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for sending emails.
        /// </summary>
        string SendMailTopicName { get; }
    }
}

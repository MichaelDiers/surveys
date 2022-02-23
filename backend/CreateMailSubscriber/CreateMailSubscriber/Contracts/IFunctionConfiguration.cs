namespace CreateMailSubscriber.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the email template collection.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for sending emails.
        /// </summary>
        string SendMailTopicName { get; }
    }
}

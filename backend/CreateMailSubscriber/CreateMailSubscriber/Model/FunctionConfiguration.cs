namespace CreateMailSubscriber.Model
{
    using CreateMailSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the email template collection.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for sending emails.
        /// </summary>
        public string SendMailTopicName { get; set; }
    }
}

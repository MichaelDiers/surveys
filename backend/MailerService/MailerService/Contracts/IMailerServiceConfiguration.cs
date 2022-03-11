namespace MailerService.Contracts
{
    using MailerService.Model;

    /// <summary>
    ///     Specification of the application configuration.
    /// </summary>
    public interface IMailerServiceConfiguration
    {
        /// <summary>
        ///     Gets the sender data of emails.
        /// </summary>
        public MailboxAddressFromConfiguration MailboxAddressFrom { get; }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the configuration for the smtp client.
        /// </summary>
        public Smtp Smtp { get; }

        /// <summary>
        ///     Gets the Pub/Sub topic name for updating the status of a survey.
        /// </summary>
        string TopicName { get; }
    }
}

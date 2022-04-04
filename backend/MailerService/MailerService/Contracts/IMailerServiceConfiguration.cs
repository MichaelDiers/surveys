namespace MailerService.Contracts
{
    using MailerService.Model;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Specification of the application configuration.
    /// </summary>
    public interface IMailerServiceConfiguration : IPubSubClientEnvironment, ISecretManagerEnvironment
    {
        /// <summary>
        ///     Gets the configuration for the smtp client.
        /// </summary>
        public Smtp Smtp { get; }
    }
}

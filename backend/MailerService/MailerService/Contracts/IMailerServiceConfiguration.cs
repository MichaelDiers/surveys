namespace MailerService.Contracts
{
    using MailerService.Model;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudSecrets.Contracts.Logic;

    /// <summary>
    ///     Specification of the application configuration.
    /// </summary>
    public interface IMailerServiceConfiguration : IRuntimeEnvironment, ISecretManagerEnvironment
    {
        /// <summary>
        ///     Gets the configuration for the smtp client.
        /// </summary>
        public Smtp Smtp { get; }
    }
}

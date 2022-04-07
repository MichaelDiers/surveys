namespace MailerService.Contracts
{
    using MailerService.Model;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;

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

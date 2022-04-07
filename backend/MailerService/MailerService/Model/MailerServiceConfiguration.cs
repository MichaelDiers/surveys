namespace MailerService.Model
{
    using MailerService.Contracts;
    using Md.Common.Model;

    /// <summary>
    ///     Specifies the application configuration.
    /// </summary>
    public class MailerServiceConfiguration : RuntimeEnvironment, IMailerServiceConfiguration
    {
        /// <summary>
        ///     Gets or sets the connection data for smtp.
        /// </summary>
        public Smtp Smtp { get; set; } = new Smtp();
    }
}

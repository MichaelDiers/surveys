namespace MailerService.Model
{
    using MailerService.Contracts;

    /// <summary>
    ///     Specifies the smtp connection data.
    /// </summary>
    public class Smtp : ISmtp
    {
        /// <summary>
        ///     Gets the display name for <see cref="UserNameKey" />.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the smtp password.
        /// </summary>
        public string PasswordKey { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the smtp port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets the smtp server.
        /// </summary>
        public string Server { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        public string UserNameKey { get; set; } = string.Empty;
    }
}

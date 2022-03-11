namespace MailerService.Model
{
    using MailerService.Contracts;

    /// <summary>
    ///     Specifies the smtp connection data.
    /// </summary>
    public class Smtp : ISmtp
    {
        /// <summary>
        ///     Gets or sets the smtp password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

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
        public string UserName { get; set; } = string.Empty;
    }
}

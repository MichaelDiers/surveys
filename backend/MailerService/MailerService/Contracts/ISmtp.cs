namespace MailerService.Contracts
{
    /// <summary>
    ///     Specifies the smtp connection data.
    /// </summary>
    public interface ISmtp
    {
        /// <summary>
        ///     Gets the display name for <see cref="UserNameKey" />.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the smtp password.
        /// </summary>
        string PasswordKey { get; }

        /// <summary>
        ///     Gets the smtp port.
        /// </summary>
        int Port { get; }

        /// <summary>
        ///     Gets the smtp server.
        /// </summary>
        string Server { get; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        string UserNameKey { get; }
    }
}

namespace MailerService.Contracts
{
    using System.Threading.Tasks;
    using MimeKit;

    /// <summary>
    ///     Specifies operations on smtp clients.
    /// </summary>
    public interface IMailerSmtpClient
    {
        /// <summary>
        ///     Sends an email message.
        /// </summary>
        /// <param name="message">The message to be send.</param>
        /// <param name="smtp">The smtp connection data.</param>
        /// <param name="userName">The smtp user name.</param>
        /// <param name="password">The smtp password.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task SendAsync(
            MimeMessage message,
            ISmtp smtp,
            string userName,
            string password
        );
    }
}

namespace MailerService.Model
{
    /// <summary>
    ///     Specifies the email sender configuration.
    /// </summary>
    public class MailboxAddressFromConfiguration
    {
        /// <summary>
        ///     Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the recipient.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

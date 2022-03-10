namespace Surveys.Common.Messages
{
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Specifies an email recipient.
    /// </summary>
    public class Recipient : IRecipient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Recipient" />.
        /// </summary>
        /// <param name="email">The email of the recipient.</param>
        /// <param name="name">The name of the recipient.</param>
        public Recipient(string email, string name)
        {
            this.Email = email.ValidateIsAnEmail(nameof(email));
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
        }

        /// <summary>
        ///     Gets or sets the email address.
        /// </summary>
        [JsonProperty("email", Required = Required.Always, Order = 1)]
        public string Email { get; }

        /// <summary>
        ///     Gets or sets the name of the recipient.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 2)]
        public string Name { get; }
    }
}

namespace Surveys.Common.Messages
{
    using Md.Common.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    ///     Specifies the body data of an email.
    /// </summary>
    public class Body : IBody
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Body" />.
        /// </summary>
        /// <param name="html">The html part of the body.</param>
        /// <param name="plain">The plain text part of the body.</param>
        [JsonConstructor]
        public Body(string html, string plain)
        {
            this.Html = html.ValidateIsNotNullOrWhitespace(nameof(html));
            this.Plain = plain.ValidateIsNotNullOrWhitespace(nameof(plain));
        }

        /// <summary>
        ///     Gets or sets the html content.
        /// </summary>
        [JsonProperty("html", Required = Required.Always, Order = 1)]
        public string Html { get; }

        /// <summary>
        ///     Gets or sets the plain text content.
        /// </summary>
        [JsonProperty("plain", Required = Required.Always, Order = 2)]
        public string Plain { get; }
    }
}

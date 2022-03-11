namespace Surveys.Common.Contracts.Messages
{
    /// <summary>
    ///     Specifies the body data of an email.
    /// </summary>
    public interface IBody
    {
        /// <summary>
        ///     Gets or sets the html content.
        /// </summary>
        string Html { get; }

        /// <summary>
        ///     Gets or sets the plain text content.
        /// </summary>
        string Plain { get; }
    }
}

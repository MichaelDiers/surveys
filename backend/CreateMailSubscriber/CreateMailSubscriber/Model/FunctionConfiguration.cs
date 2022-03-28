namespace CreateMailSubscriber.Model
{
    using CreateMailSubscriber.Contracts;
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the url format for the survey front end.
        /// </summary>
        public string FrondEndUrlFormat { get; set; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for sending emails.
        /// </summary>
        public string SendMailTopicName { get; set; }
    }
}

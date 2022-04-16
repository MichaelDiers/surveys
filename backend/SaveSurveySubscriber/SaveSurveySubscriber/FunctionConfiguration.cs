namespace SaveSurveySubscriber
{
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the pub/sub topic name for saving survey results.
        /// </summary>
        public string SaveSurveyResultTopicName { get; set; } = string.Empty;
    }
}

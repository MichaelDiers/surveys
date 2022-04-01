namespace SaveSurveyResultSubscriber.Model
{
    using Md.Common.Model;
    using SaveSurveyResultSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        public string TopicName { get; set; }
    }
}

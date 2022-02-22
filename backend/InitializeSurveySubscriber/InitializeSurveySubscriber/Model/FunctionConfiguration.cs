namespace InitializeSurveySubscriber.Model
{
    using InitializeSurveySubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving survey results.
        /// </summary>
        public string SaveSurveyResultTopicName { get; set; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving surveys.
        /// </summary>
        public string SaveSurveyTopicName { get; set; }
    }
}

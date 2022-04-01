namespace InitializeSurveySubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the pub/sub topic for creating emails.
        /// </summary>
        string CreateMailTopicName { get; }

        /// <summary>
        ///     Gets the runtime environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving survey results.
        /// </summary>
        string SaveSurveyResultTopicName { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving survey status updates.
        /// </summary>
        string SaveSurveyStatusTopicName { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving surveys.
        /// </summary>
        string SaveSurveyTopicName { get; }
    }
}

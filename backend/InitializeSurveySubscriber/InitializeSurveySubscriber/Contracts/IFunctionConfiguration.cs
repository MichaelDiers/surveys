namespace InitializeSurveySubscriber.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic for saving surveys.
        /// </summary>
        string SaveSurveyTopicName { get; }
    }
}

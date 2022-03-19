namespace SaveSurveyResultSubscriber.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the survey collection.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        string PubSubTopicName { get; }
    }
}

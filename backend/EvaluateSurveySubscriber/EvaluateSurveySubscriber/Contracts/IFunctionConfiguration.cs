namespace EvaluateSurveySubscriber.Contracts
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
        ///     Gets the pub/sub topic name.
        /// </summary>
        string SaveSurveyStatusTopicName { get; }

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        string SurveyClosedTopicName { get; }

        /// <summary>
        ///     Gets the name of the survey results collection.
        /// </summary>
        string SurveyResultsCollectionName { get; }

        /// <summary>
        ///     Gets the name of the surveys collection.
        /// </summary>
        string SurveysCollectionName { get; }

        /// <summary>
        ///     Gets the name of the survey status collection.
        /// </summary>
        string SurveyStatusCollectionName { get; }
    }
}

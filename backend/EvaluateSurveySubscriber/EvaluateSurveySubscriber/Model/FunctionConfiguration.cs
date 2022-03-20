namespace EvaluateSurveySubscriber.Model
{
    using EvaluateSurveySubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; set; } = "";

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        public string SaveSurveyStatusTopicName { get; set; } = "";

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        public string SurveyClosedTopicName { get; set; } = "";

        /// <summary>
        ///     Gets the name of the survey results collection.
        /// </summary>
        public string SurveyResultsCollectionName { get; set; } = "";

        /// <summary>
        ///     Gets the name of the surveys collection.
        /// </summary>
        public string SurveysCollectionName { get; set; } = "";

        /// <summary>
        ///     Gets the name of the survey status collection.
        /// </summary>
        public string SurveyStatusCollectionName { get; set; } = "";
    }
}

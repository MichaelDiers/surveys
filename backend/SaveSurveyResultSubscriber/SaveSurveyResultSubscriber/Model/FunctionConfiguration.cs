namespace SaveSurveyResultSubscriber.Model
{
    using SaveSurveyResultSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the survey collection.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; set; }
    }
}

namespace EvaluateSurveySubscriber
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        string SaveSurveyStatusTopicName { get; }

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        string SurveyClosedTopicName { get; }
    }
}

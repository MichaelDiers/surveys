namespace EvaluateSurveySubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the environment.
        /// </summary>
        Environment Environment { get; }

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

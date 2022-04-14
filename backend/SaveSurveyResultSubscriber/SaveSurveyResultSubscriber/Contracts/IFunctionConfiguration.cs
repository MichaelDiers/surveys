namespace SaveSurveyResultSubscriber.Contracts
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the pub/sub topic name for creating thank you emails.
        /// </summary>
        string CreateMailTopicName { get; }

        /// <summary>
        ///     Gets the pub/sub topic name for evaluating surveys.
        /// </summary>
        string EvaluateSurveyTopicName { get; }
    }
}

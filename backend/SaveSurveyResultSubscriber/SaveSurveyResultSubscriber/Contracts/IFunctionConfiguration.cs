namespace SaveSurveyResultSubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        string PubSubTopicName { get; }
    }
}

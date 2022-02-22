namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Surveys.Common.PubSub.Contracts;
    using Surveys.Common.PubSub.Logic;

    /// <summary>
    ///     Access google pub/sub for saving survey results.
    /// </summary>
    public class SaveSurveyResultPubSub : PubSub, ISaveSurveyResultPubSub
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSub" />.
        /// </summary>
        /// <param name="configuration">The configuration for sending messages to pub/sub.</param>
        public SaveSurveyResultPubSub(IPubSubConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

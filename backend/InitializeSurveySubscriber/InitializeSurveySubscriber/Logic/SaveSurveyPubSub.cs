namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Surveys.Common.PubSub.Contracts;
    using Surveys.Common.PubSub.Logic;

    /// <summary>
    ///     Access google pub/sub for saving surveys.
    /// </summary>
    public class SaveSurveyPubSub : PubSub, ISaveSurveyPubSub
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSub" />.
        /// </summary>
        /// <param name="configuration">The configuration for sending messages to pub/sub.</param>
        public SaveSurveyPubSub(IPubSubConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

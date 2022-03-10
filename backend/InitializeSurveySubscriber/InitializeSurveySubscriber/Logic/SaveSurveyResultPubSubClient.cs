namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending save survey result messages.
    /// </summary>
    public class SaveSurveyResultPubSubClient : PubSubClient, ISaveSurveyResultPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultPubSubClient" />.
        /// </summary>
        /// <param name="configuration"></param>
        public SaveSurveyResultPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending save survey messages.
    /// </summary>
    public class SaveSurveyPubSubClient : PubSubClient, ISaveSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyPubSubClient" />.
        /// </summary>
        /// <param name="configuration"></param>
        public SaveSurveyPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

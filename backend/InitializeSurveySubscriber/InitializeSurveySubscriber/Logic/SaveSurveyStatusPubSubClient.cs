namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending save survey status messages.
    /// </summary>
    public class SaveSurveyStatusPubSubClient : PubSubClient, ISaveSurveyStatusPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusPubSubClient" />.
        /// </summary>
        /// <param name="configuration"></param>
        public SaveSurveyStatusPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

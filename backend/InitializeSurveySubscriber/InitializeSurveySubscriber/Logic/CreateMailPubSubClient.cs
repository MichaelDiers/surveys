namespace InitializeSurveySubscriber.Logic
{
    using InitializeSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending create mail messages.
    /// </summary>
    public class CreateMailPubSubClient : PubSubClient, ICreateMailPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CreateMailPubSubClient" />.
        /// </summary>
        /// <param name="configuration"></param>
        public CreateMailPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

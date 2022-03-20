namespace EvaluateSurveySubscriber.Logic
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to google cloud pub/sub for sending survey closed messages.
    /// </summary>
    public class SurveyClosedPubSubClient : PubSubClient, ISurveyClosedPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyClosedPubSubClient" />.
        /// </summary>
        /// <param name="configuration">The pub/sub client configuration.</param>
        public SurveyClosedPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

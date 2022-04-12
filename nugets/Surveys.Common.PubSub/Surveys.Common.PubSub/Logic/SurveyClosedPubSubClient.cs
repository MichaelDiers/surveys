namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISurveyClosedMessage" />.
    /// </summary>
    public class SurveyClosedPubSubClient : AbstractPubSubClient<ISurveyClosedMessage>, ISurveyClosedPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SurveyClosedPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

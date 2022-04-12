namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IEvaluateSurveyMessage" />.
    /// </summary>
    public class EvaluateSurveyPubSubClient : AbstractPubSubClient<IEvaluateSurveyMessage>, IEvaluateSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="IEvaluateSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public EvaluateSurveyPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

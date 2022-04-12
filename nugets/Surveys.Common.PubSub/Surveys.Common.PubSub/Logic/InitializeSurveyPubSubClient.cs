namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IInitializeSurveyMessage" />.
    /// </summary>
    public class InitializeSurveyPubSubClient
        : AbstractPubSubClient<IInitializeSurveyMessage>, IInitializeSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="InitializeSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public InitializeSurveyPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

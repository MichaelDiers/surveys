namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveSurveyMessage" />.
    /// </summary>
    public class SaveSurveyPubSubClient : AbstractPubSubClient<ISaveSurveyMessage>, ISaveSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveSurveyPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

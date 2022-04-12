namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveSurveyStatusMessage" />.
    /// </summary>
    public class SaveSurveyStatusPubSubClient
        : AbstractPubSubClient<ISaveSurveyStatusMessage>, ISaveSurveyStatusPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveSurveyStatusPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

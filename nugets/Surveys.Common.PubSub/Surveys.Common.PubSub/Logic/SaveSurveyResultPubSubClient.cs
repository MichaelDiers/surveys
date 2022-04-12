namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveSurveyResultMessage" />.
    /// </summary>
    public class SaveSurveyResultPubSubClient
        : AbstractPubSubClient<ISaveSurveyResultMessage>, ISaveSurveyResultPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveSurveyResultPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}

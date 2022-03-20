namespace EvaluateSurveySubscriber.Logic
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access to google cloud pub/sub for saving the status of a survey.
    /// </summary>
    public class SaveSurveyStatusPubSubClient : PubSubClient, ISaveSurveyStatusPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusPubSubClient" />.
        /// </summary>
        /// <param name="configuration">The pub/sub client configuration.</param>
        public SaveSurveyStatusPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

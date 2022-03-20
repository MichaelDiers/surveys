namespace EvaluateSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to google cloud pub/sub for saving the status of a survey.
    /// </summary>
    public interface ISaveSurveyStatusPubSubClient : IPubSubClient
    {
    }
}

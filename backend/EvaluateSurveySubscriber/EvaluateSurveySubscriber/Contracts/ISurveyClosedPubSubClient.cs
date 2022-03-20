namespace EvaluateSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to google cloud pub/sub for sending survey closed messages.
    /// </summary>
    public interface ISurveyClosedPubSubClient : IPubSubClient
    {
    }
}

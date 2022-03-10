namespace InitializeSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending save survey messages.
    /// </summary>
    public interface ISaveSurveyPubSubClient : IPubSubClient
    {
    }
}

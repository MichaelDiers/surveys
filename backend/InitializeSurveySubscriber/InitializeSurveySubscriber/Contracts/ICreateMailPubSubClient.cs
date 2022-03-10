namespace InitializeSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to the pub/sub client for sending create mail messages.
    /// </summary>
    public interface ICreateMailPubSubClient : IPubSubClient
    {
    }
}

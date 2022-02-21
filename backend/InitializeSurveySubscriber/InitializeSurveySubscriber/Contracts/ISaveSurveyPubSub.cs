namespace InitializeSurveySubscriber.Contracts
{
    using Surveys.Common.PubSub.Contracts;

    /// <summary>
    ///     Access google pub/sub for saving surveys.
    /// </summary>
    public interface ISaveSurveyPubSub : IPubSub
    {
    }
}

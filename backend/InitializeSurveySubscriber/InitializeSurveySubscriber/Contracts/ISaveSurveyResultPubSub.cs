namespace InitializeSurveySubscriber.Contracts
{
    using Surveys.Common.PubSub.Contracts;

    /// <summary>
    ///     Access google pub/sub for saving survey results.
    /// </summary>
    public interface ISaveSurveyResultPubSub : IPubSub
    {
    }
}

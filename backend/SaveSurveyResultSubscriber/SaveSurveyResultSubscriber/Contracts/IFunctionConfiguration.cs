namespace SaveSurveyResultSubscriber.Contracts
{
    using Md.Common.Contracts;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
    }
}

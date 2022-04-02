namespace CreateMailSubscriber.Contracts
{
    using Md.Common.Contracts;
    using Md.GoogleCloudPubSub.Logic;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
        /// <summary>
        ///     Gets the url format for the survey front end.
        /// </summary>
        string FrondEndUrlFormat { get; }
    }
}

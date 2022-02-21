namespace Surveys.Common.PubSub.Contracts
{
    /// <summary>
    ///     The configuration for sending messages to pub/sub.
    /// </summary>
    public interface IPubSubConfiguration
    {
        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        string TopicName { get; }
    }
}

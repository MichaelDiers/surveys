namespace Surveys.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISendMailMessage" />.
    /// </summary>
    public interface ISendMailPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="ISendMailMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        Task PublishAsync(ISendMailMessage message);
    }
}

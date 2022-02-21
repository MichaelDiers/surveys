namespace Surveys.Common.PubSub.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Access google pub/sub.
    /// </summary>
    public interface IPubSub
    {
        /// <summary>
        ///     Publish a message to a pub/sub topic.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message that is sent to pub/sub.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task PublishAsync<T>(T message);
    }
}

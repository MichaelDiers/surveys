namespace Surveys.Common.PubSub.Logic
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.PubSub.V1;
    using Newtonsoft.Json;
    using Surveys.Common.PubSub.Contracts;

    /// <summary>
    ///     Access google pub/sub.
    /// </summary>
    public class PubSub : IPubSub
    {
        /// <summary>
        ///     The configuration for sending messages to pub/sub.
        /// </summary>
        private readonly IPubSubConfiguration configuration;

        /// <summary>
        ///     Access google cloud Pub/Sub.
        /// </summary>
        private PublisherClient? client;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSub" />.
        /// </summary>
        /// <param name="configuration">The configuration for sending messages to pub/sub.</param>
        public PubSub(IPubSubConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Publish a message to a pub/sub topic.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message that is sent to pub/sub.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task PublishAsync<T>(T message)
        {
            if (this.client == null)
            {
                var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicName);
                this.client = await PublisherClient.CreateAsync(topic);
            }

            var json = JsonConvert.SerializeObject(message);
            await this.client.PublishAsync(json);
        }
    }
}

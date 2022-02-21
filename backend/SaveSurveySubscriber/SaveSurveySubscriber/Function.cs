namespace SaveSurveySubscriber
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Framework;
    using Google.Cloud.Functions.Hosting;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SaveSurveySubscriber.Contracts;
    using SaveSurveySubscriber.Model;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : ICloudEventFunction<MessagePublishedData>
    {
        /// <summary>
        ///     The error logger writes to the google cloud..
        /// </summary>
        private readonly ILogger<Function> logger;

        /// <summary>
        ///     Provider for handling the business logic.
        /// </summary>
        private readonly IFunctionProvider provider;

        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="provider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IFunctionProvider provider)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        ///     Handle incoming messages from google cloud pub/sub.
        /// </summary>
        /// <param name="cloudEvent">The raised event.</param>
        /// <param name="data">The data send with the message.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" />.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public async Task HandleAsync(
            CloudEvent cloudEvent,
            MessagePublishedData data,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var json = data?.Message?.TextData;
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new ArgumentException(
                        "Empty incoming json message",
                        nameof(MessagePublishedData.Message.TextData));
                }

                var message = JsonConvert.DeserializeObject<Message>(json);
                if (message == null)
                {
                    throw new ArgumentException(
                        $"Cannot deserialize json to ${nameof(Message)}: '{json}'",
                        nameof(MessagePublishedData.Message.TextData));
                }

                await this.provider.HandleAsync(message);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Unexpected error!");
            }
        }
    }
}

namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ICreateMailMessage, Function>
    {
        /// <summary>
        ///     Access the pub/sub client for sending emails.
        /// </summary>
        private readonly IPubSubClient sendMailPubSubClient;


        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="sendMailPubSubClient">Access the pub/sub client for sending emails.</param>
        public FunctionProvider(ILogger<Function> logger, IPubSubClient sendMailPubSubClient)
            : base(logger)
        {
            this.sendMailPubSubClient =
                sendMailPubSubClient ?? throw new ArgumentNullException(nameof(sendMailPubSubClient));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override Task HandleMessageAsync(ICreateMailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return Task.CompletedTask;
        }
    }
}

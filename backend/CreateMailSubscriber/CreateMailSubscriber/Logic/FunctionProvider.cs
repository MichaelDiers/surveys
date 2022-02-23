namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using CreateMailSubscriber.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Access the application settings.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Access the pub/sub client for sending emails.
        /// </summary>
        private readonly IPubSub sendMailPubSub;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="configuration">Access to the application settings.</param>
        /// <param name="sendMailPubSub">Access the pub/sub client for sending emails.</param>
        public FunctionProvider(IFunctionConfiguration configuration, IPubSub sendMailPubSub)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.sendMailPubSub = sendMailPubSub ?? throw new ArgumentNullException(nameof(sendMailPubSub));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public Task HandleAsync(ICreateMailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return Task.CompletedTask;
        }
    }
}

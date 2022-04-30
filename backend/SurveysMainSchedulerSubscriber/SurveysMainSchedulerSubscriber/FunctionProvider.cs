namespace SurveysMainSchedulerSubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;
    using Md.Common.Messages;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IMessage, Function>
    {
        /// <summary>
        ///     The scheduler client are triggered by the scheduler.
        /// </summary>
        private readonly IEnumerable<ISchedulerPubSubClient> schedulerPubSubClients;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="schedulerPubSubClients">The scheduler client are triggered by the scheduler.</param>
        public FunctionProvider(ILogger<Function> logger, IEnumerable<ISchedulerPubSubClient> schedulerPubSubClients)
            : base(logger)
        {
            this.schedulerPubSubClients = schedulerPubSubClients;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IMessage message)
        {
            var processId = Guid.NewGuid().ToString();
            foreach (var schedulerPubSubClient in this.schedulerPubSubClients)
            {
                await schedulerPubSubClient.PublishAsync(new Message(processId));
            }
        }
    }
}

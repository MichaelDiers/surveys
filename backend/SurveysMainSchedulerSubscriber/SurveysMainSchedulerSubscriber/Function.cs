namespace SurveysMainSchedulerSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Messages;
    using Md.Common.Messages;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : PubSubFunction<Message, IMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="provider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IPubSubProvider<IMessage> provider)
            : base(logger, provider)
        {
        }
    }
}

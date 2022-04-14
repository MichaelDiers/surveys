namespace CreateMailSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : PubSubFunction<CreateMailMessage, ICreateMailMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="provider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IPubSubProvider<ICreateMailMessage> provider)
            : base(logger, provider)
        {
        }
    }
}

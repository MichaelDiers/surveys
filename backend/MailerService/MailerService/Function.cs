namespace MailerService
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Google cloud function for sending emails using pub/sub.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : PubSubFunction<SendMailMessage, ISendMailMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">Logger for error messages.</param>
        /// <param name="provider">Provider for sending emails.</param>
        public Function(ILogger<Function> logger, IPubSubProvider<ISendMailMessage> provider)
            : base(logger, provider)
        {
        }
    }
}

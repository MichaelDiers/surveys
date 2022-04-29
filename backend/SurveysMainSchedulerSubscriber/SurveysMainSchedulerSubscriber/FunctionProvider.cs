namespace SurveysMainSchedulerSubscriber
{
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        public FunctionProvider(ILogger<Function> logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IMessage message)
        {
            await Task.CompletedTask;
        }
    }
}

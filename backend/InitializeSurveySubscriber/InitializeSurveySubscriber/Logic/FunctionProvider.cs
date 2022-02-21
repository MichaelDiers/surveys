namespace InitializeSurveySubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;

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
        ///     Access the pub/sub client for saving surveys.
        /// </summary>
        private readonly ISaveSurveyPubSub saveSurveyPubSub;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="configuration">Access to the application settings.</param>
        /// <param name="saveSurveyPubSub">Access the pub/sub client for saving surveys.</param>
        public FunctionProvider(IFunctionConfiguration configuration, ISaveSurveyPubSub saveSurveyPubSub)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.saveSurveyPubSub = saveSurveyPubSub ?? throw new ArgumentNullException(nameof(saveSurveyPubSub));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public async Task HandleAsync(IInitializeSurveyMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var internalSurveyId = Guid.NewGuid().ToString();

            await this.saveSurveyPubSub.PublishAsync(
                new SaveSurveyMessage(message.Survey, internalSurveyId, message.ProcessId));
        }
    }
}

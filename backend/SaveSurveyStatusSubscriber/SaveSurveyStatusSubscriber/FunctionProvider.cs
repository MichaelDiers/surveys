namespace SaveSurveyStatusSubscriber
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISaveSurveyStatusMessage, Function>
    {
        /// <summary>
        ///     Access to the survey status database.
        /// </summary>
        private readonly ISurveyStatusDatabase database;

        /// <summary>
        ///     Send a message for indicating that the survey has ended.
        /// </summary>
        private readonly ISurveyClosedPubSubClient surveyClosedPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the survey status database.</param>
        /// <param name="surveyClosedPubSubClient">Send a message for indicating that the survey has ended.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyStatusDatabase database,
            ISurveyClosedPubSubClient surveyClosedPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.surveyClosedPubSubClient = surveyClosedPubSubClient;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ISaveSurveyStatusMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var documentId = await this.database.InsertIfNotExistsAsync(message.SurveyStatus);
            if (documentId != null)
            {
                await this.surveyClosedPubSubClient.PublishAsync(message.SurveyClosedMessage);
            }
        }
    }
}

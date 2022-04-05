namespace SaveSurveyResultSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISaveSurveyResultMessage, Function>
    {
        /// <summary>
        ///     Access google cloud pub/sub.
        /// </summary>
        private readonly ICreateMailPubSubClient createMailPubSubClient;

        /// <summary>
        ///     Access to the survey result database.
        /// </summary>
        private readonly ISurveyResultDatabase database;

        /// <summary>
        ///     Access google cloud pub/sub.
        /// </summary>
        private readonly IEvaluateSurveyPubSubClient evaluateSurveyPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the survey result database.</param>
        /// <param name="evaluateSurveyPubSubClient">Access google cloud pub/sub.</param>
        /// <param name="createMailPubSubClient">Access google cloud pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyResultDatabase database,
            IEvaluateSurveyPubSubClient evaluateSurveyPubSubClient,
            ICreateMailPubSubClient createMailPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.evaluateSurveyPubSubClient = evaluateSurveyPubSubClient;
            this.createMailPubSubClient = createMailPubSubClient;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ISaveSurveyResultMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var documentId = await this.database.InsertAsync(message.SurveyResult);

            if (!message.SurveyResult.IsSuggested)
            {
                await this.evaluateSurveyPubSubClient.PublishAsync(
                    new EvaluateSurveyMessage(message.ProcessId, message.SurveyResult.InternalSurveyId));
                await this.createMailPubSubClient.PublishAsync(
                    new CreateMailMessage(
                        message.ProcessId,
                        MailType.ThankYou,
                        null,
                        documentId));
            }
        }
    }
}

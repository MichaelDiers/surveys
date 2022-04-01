namespace SaveSurveyResultSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISaveSurveyResultMessage, Function>
    {
        /// <summary>
        ///     Access to the survey result database.
        /// </summary>
        private readonly ISurveyResultDatabase database;

        /// <summary>
        ///     Access google cloud pub/sub.
        /// </summary>
        private readonly IEvaluateSurveyPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the survey result database.</param>
        /// <param name="pubSubClient">Access google cloud pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyResultDatabase database,
            IEvaluateSurveyPubSubClient pubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.pubSubClient = pubSubClient;
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

            await this.database.InsertAsync(message.SurveyResult);
            await this.pubSubClient.PublishAsync(
                new EvaluateSurveyMessage(message.ProcessId, message.SurveyResult.InternalSurveyId));
        }
    }
}

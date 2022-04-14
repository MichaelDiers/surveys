namespace SaveSurveyResultSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
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

        private readonly ISurveyReadOnlyDatabase surveyReadOnlyDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the survey result database.</param>
        /// <param name="surveyReadOnlyDatabase">Access the database collection surveys.</param>
        /// <param name="evaluateSurveyPubSubClient">Access google cloud pub/sub.</param>
        /// <param name="createMailPubSubClient">Access google cloud pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyResultDatabase database,
            ISurveyReadOnlyDatabase surveyReadOnlyDatabase,
            IEvaluateSurveyPubSubClient evaluateSurveyPubSubClient,
            ICreateMailPubSubClient createMailPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.surveyReadOnlyDatabase = surveyReadOnlyDatabase ??
                                          throw new ArgumentNullException(nameof(surveyReadOnlyDatabase));
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
            var survey = await this.surveyReadOnlyDatabase.ReadByDocumentIdAsync(message.SurveyResult.ParentDocumentId);
            if (survey == null)
            {
                await this.LogErrorAsync(
                    new ArgumentException(
                        $"Cannot find survey for survey result: {Serializer.SerializeObject(message)}",
                        nameof(message.SurveyResult)),
                    "Cannot find survey.");
                return;
            }

            if (survey.Participants.All(participant => participant.Id != message.SurveyResult.ParticipantId))
            {
                await this.LogErrorAsync(
                    new ArgumentException(
                        $"Participant is not part of the survey: {Serializer.SerializeObject(message)}",
                        nameof(message.SurveyResult.ParticipantId)),
                    "Cannot find participant");
                return;
            }

            var surveyResult = new SurveyResult(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                survey.DocumentId,
                message.SurveyResult.ParticipantId,
                message.SurveyResult.IsSuggested,
                message.SurveyResult.Results);
            await this.database.InsertAsync(surveyResult.DocumentId, surveyResult);

            if (!message.SurveyResult.IsSuggested)
            {
                // await this.evaluateSurveyPubSubClient.PublishAsync(
                //    new EvaluateSurveyMessage(message.ProcessId, message.SurveyResult.InternalSurveyId));
                await this.createMailPubSubClient.PublishAsync(
                    new CreateMailMessage(
                        message.ProcessId,
                        MailType.ThankYou,
                        survey,
                        surveyResult));
            }
        }
    }
}

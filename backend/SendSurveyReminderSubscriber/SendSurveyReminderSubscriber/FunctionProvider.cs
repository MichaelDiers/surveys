namespace SendSurveyReminderSubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;
    using Md.Common.Database;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IMessage, Function>
    {
        /// <summary>
        ///     Client for sending pub/sub messages for creating emails.
        /// </summary>
        private readonly ICreateMailPubSubClient createMailPubSubClient;

        /// <summary>
        ///     Access to the survey database collection.
        /// </summary>
        private readonly ISurveyReadOnlyDatabase surveyReadOnlyDatabase;

        /// <summary>
        ///     Access to the survey results database collection.
        /// </summary>
        private readonly ISurveyResultReadOnlyDatabase surveyResultReadOnlyDatabase;

        /// <summary>
        ///     Access to the survey status database collection.
        /// </summary>
        private readonly ISurveyStatusReadOnlyDatabase surveyStatusReadOnlyDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="surveyReadOnlyDatabase">Access to the survey database collection.</param>
        /// <param name="surveyStatusReadOnlyDatabase">Access to the survey status database collection.</param>
        /// <param name="surveyResultReadOnlyDatabase">Access to the survey results database collection.</param>
        /// <param name="createMailPubSubClient">Client for sending pub/sub messages for creating emails.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyReadOnlyDatabase surveyReadOnlyDatabase,
            ISurveyStatusReadOnlyDatabase surveyStatusReadOnlyDatabase,
            ISurveyResultReadOnlyDatabase surveyResultReadOnlyDatabase,
            ICreateMailPubSubClient createMailPubSubClient
        )
            : base(logger)
        {
            this.surveyReadOnlyDatabase = surveyReadOnlyDatabase;
            this.surveyStatusReadOnlyDatabase = surveyStatusReadOnlyDatabase;
            this.surveyResultReadOnlyDatabase = surveyResultReadOnlyDatabase;
            this.createMailPubSubClient = createMailPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IMessage message)
        {
            var surveys = await this.ReadOpenSurveys();
            foreach (var survey in surveys.Where(survey => survey.Created.AddHours(24) < DateTime.Now))
            {
                var results = (await this.surveyResultReadOnlyDatabase.ReadManyAsync(
                    DatabaseObject.ParentDocumentIdName,
                    survey.DocumentId)).ToArray();
                var surveyParticipants = survey.Participants.Where(
                        participant => results.All(result => result.ParticipantId != participant.Id))
                    .Select(participant => participant.Id)
                    .ToArray();
                await this.createMailPubSubClient.PublishAsync(
                    new CreateMailMessage(
                        message.ProcessId,
                        MailType.Reminder,
                        survey,
                        null,
                        surveyParticipants));
            }
        }

        /// <summary>
        ///     Read all open surveys.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result contains all open surveys.</returns>
        private async Task<IEnumerable<ISurvey>> ReadOpenSurveys()
        {
            var surveysTask = this.surveyReadOnlyDatabase.ReadManyAsync();
            var surveyStatusTask = this.surveyStatusReadOnlyDatabase.ReadManyAsync();

            var surveys = (await surveysTask).ToArray();
            var surveysStatus = (await surveyStatusTask).ToArray();

            return surveys.Where(
                survey => !surveysStatus.Any(
                    status => survey.DocumentId == status.ParentDocumentId && status.Status == Status.Closed));
        }
    }
}

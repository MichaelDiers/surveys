namespace EvaluateSurveySubscriber.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IEvaluateSurveyMessage, Function>
    {
        private readonly ILogger<Function> logger;

        /// <summary>
        ///     Access to google cloud pub/sub for saving the status of a survey.
        /// </summary>
        private readonly ISaveSurveyStatusPubSubClient saveSurveyStatusPubSubClient;

        /// <summary>
        ///     Access to google cloud pub/sub for sending survey closed messages.
        /// </summary>
        private readonly ISurveyClosedPubSubClient surveyClosedPubSubClient;

        /// <summary>
        ///     Access to the survey database collection.
        /// </summary>
        private readonly ISurveyReadOnlyDatabase surveyDatabase;

        /// <summary>
        ///     Access to the survey results database collection.
        /// </summary>
        private readonly ISurveyResultReadOnlyDatabase surveyResultsDatabase;

        /// <summary>
        ///     Access to the survey status database collection.
        /// </summary>
        private readonly ISurveyStatusReadOnlyDatabase surveyStatusDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="surveyDatabase">Access to the survey database collection.</param>
        /// <param name="surveyResultsDatabase">Access to the survey results database collection.</param>
        /// <param name="surveyStatusDatabase">Access to the survey status database collection.</param>
        /// <param name="saveSurveyStatusPubSubClient">Access to google cloud pub/sub for saving the status of a survey.</param>
        /// <param name="surveyClosedPubSubClient">Access to google cloud pub/sub for sending survey closed messages.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyReadOnlyDatabase surveyDatabase,
            ISurveyResultReadOnlyDatabase surveyResultsDatabase,
            ISurveyStatusReadOnlyDatabase surveyStatusDatabase,
            ISaveSurveyStatusPubSubClient saveSurveyStatusPubSubClient,
            ISurveyClosedPubSubClient surveyClosedPubSubClient
        )
            : base(logger)
        {
            this.logger = logger;
            this.surveyDatabase = surveyDatabase;
            this.surveyResultsDatabase = surveyResultsDatabase;
            this.surveyStatusDatabase = surveyStatusDatabase;
            this.saveSurveyStatusPubSubClient = saveSurveyStatusPubSubClient;
            this.surveyClosedPubSubClient = surveyClosedPubSubClient;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(IEvaluateSurveyMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var (survey, results, status) = await this.ReadData(message);
            if (survey == null)

            {
                throw new ArgumentException($"Unknown survey {message.InternalSurveyId}", nameof(message));
            }

            if (status.Any(s => s.Status == Status.Closed))
            {
                throw new ArgumentException($"Survey {message.InternalSurveyId} is already closed.");
            }

            var participantResults = survey.Participants.Select(
                    participant =>
                        results.LastOrDefault(result => !result.IsSuggested && result.ParticipantId == participant.Id))
                .ToArray();
            if (participantResults.Length == survey.Participants.Count() &&
                participantResults.All(result => result != null))
            {
                await this.saveSurveyStatusPubSubClient.PublishAsync(
                    new SaveSurveyStatusMessage(
                        message.ProcessId,
                        new SurveyStatus(message.InternalSurveyId, Status.Closed)));
                var surveyClosedMessage = new SurveyClosedMessage(message.ProcessId, survey, results);
                this.logger.LogInformation(JsonConvert.SerializeObject(message));
                await this.surveyClosedPubSubClient.PublishAsync(surveyClosedMessage);
            }
        }

        private async Task<(ISurvey? survey, IEnumerable<ISurveyResult> results, IEnumerable<ISurveyStatus> status)>
            ReadData(IEvaluateSurveyMessage message)
        {
            var surveyTask = this.surveyDatabase.ReadByDocumentIdAsync(message.InternalSurveyId);
            var surveyResultsTask = this.surveyResultsDatabase.ReadManyAsync(
                SurveyResult.InternalSurveyIdName,
                message.InternalSurveyId,
                OrderType.Asc);
            var surveyStatusTask = this.surveyStatusDatabase.ReadManyAsync(
                SurveyStatus.InternalSurveyIdName,
                message.InternalSurveyId);


            var survey = await surveyTask ??
                         throw new ArgumentException(
                             $"Survey {message.InternalSurveyId} not found.",
                             nameof(message.InternalSurveyId));

            var surveyResults = (await surveyResultsTask).ToArray();
            var surveyStatus = (await surveyStatusTask).ToArray();

            return (survey, surveyResults, surveyStatus);
        }
    }
}

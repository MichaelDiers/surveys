namespace EvaluateSurveySubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Database;
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
    public class FunctionProvider : PubSubProvider<IEvaluateSurveyMessage, Function>
    {
        /// <summary>
        ///     Access to google cloud pub/sub for saving the status of a survey.
        /// </summary>
        private readonly ISaveSurveyStatusPubSubClient saveSurveyStatusPubSubClient;

        /// <summary>
        ///     Access to the survey database collection.
        /// </summary>
        private readonly ISurveyReadOnlyDatabase surveyDatabase;

        /// <summary>
        ///     Access to the survey results database collection.
        /// </summary>
        private readonly ISurveyResultReadOnlyDatabase surveyResultsDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="surveyDatabase">Access to the survey database collection.</param>
        /// <param name="surveyResultsDatabase">Access to the survey results database collection.</param>
        /// <param name="saveSurveyStatusPubSubClient">Access to google cloud pub/sub for saving the status of a survey.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyReadOnlyDatabase surveyDatabase,
            ISurveyResultReadOnlyDatabase surveyResultsDatabase,
            ISaveSurveyStatusPubSubClient saveSurveyStatusPubSubClient
        )
            : base(logger)
        {
            this.surveyDatabase = surveyDatabase;
            this.surveyResultsDatabase = surveyResultsDatabase;
            this.saveSurveyStatusPubSubClient = saveSurveyStatusPubSubClient;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(IEvaluateSurveyMessage message)
        {
            var (survey, results) = await this.ReadData(message);

            // if a vote exists for each participant then close the survey and publish the results
            if (survey.Participants.All(
                    participant =>
                        results.Any(result => !result.IsSuggested && participant.Id == result.ParticipantId)))
            {
                // the survey closed message is sent if the closed status is inserted
                // otherwise the message is ignored
                // prevents race conditions
                await this.saveSurveyStatusPubSubClient.PublishAsync(
                    new SaveSurveyStatusMessage(
                        message.ProcessId,
                        new SurveyStatus(
                            null,
                            null,
                            message.SurveyDocumentId,
                            Status.Closed),
                        new SurveyClosedMessage(message.ProcessId, survey, results)));
            }
        }

        private async Task<(ISurvey survey, IList<ISurveyResult> results)> ReadData(IEvaluateSurveyMessage message)
        {
            var surveyTask = this.surveyDatabase.ReadByDocumentIdAsync(message.SurveyDocumentId);
            var surveyResultsTask = this.surveyResultsDatabase.ReadManyAsync(
                DatabaseObject.ParentDocumentIdName,
                message.SurveyDocumentId);

            var survey = await surveyTask ??
                         throw new ArgumentException(
                             $"Survey {message.SurveyDocumentId} not found.",
                             nameof(message.SurveyDocumentId));

            var dictionary = new Dictionary<string, ISurveyResult>();
            foreach (var result in (await surveyResultsTask).Where(r => !r.IsSuggested)
                     .OrderByDescending(r => r.Created))
            {
                if (!dictionary.ContainsKey(result.ParticipantId))
                {
                    dictionary.Add(result.ParticipantId, result);
                }
            }

            return (survey, dictionary.Values.Select(x => x).ToArray());
        }
    }
}

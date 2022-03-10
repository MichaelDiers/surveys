namespace InitializeSurveySubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IInitializeSurveyMessage, Function>
    {
        /// <summary>
        ///     Access the pub/sub client for sending create mail messages.
        /// </summary>
        private readonly IPubSubClient createMailPubSubClient;

        /// <summary>
        ///     Access the pub/sub client for saving surveys.
        /// </summary>
        private readonly IPubSubClient saveSurveyPubSubClient;

        /// <summary>
        ///     Access the pub/sub client for saving survey results.
        /// </summary>
        private readonly IPubSubClient saveSurveyResultPubSubClient;

        /// <summary>
        ///     Access the pub/sub client for saving survey status updates.
        /// </summary>
        private readonly IPubSubClient saveSurveyStatusPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="saveSurveyPubSubClient">Access the pub/sub client for saving surveys.</param>
        /// <param name="saveSurveyResultPubSubClient">Access the pub/sub client for saving survey results.</param>
        /// <param name="saveSurveyStatusPubSubClient">Access the pub/sub client for saving survey status updates.</param>
        /// <param name="createMailPubSubClient">Access the pub/sub client for creating emails.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISaveSurveyPubSubClient saveSurveyPubSubClient,
            ISaveSurveyResultPubSubClient saveSurveyResultPubSubClient,
            ISaveSurveyStatusPubSubClient saveSurveyStatusPubSubClient,
            ICreateMailPubSubClient createMailPubSubClient
        )
            : base(logger)
        {
            this.saveSurveyPubSubClient = saveSurveyPubSubClient ??
                                          throw new ArgumentNullException(nameof(saveSurveyPubSubClient));
            this.saveSurveyResultPubSubClient = saveSurveyResultPubSubClient ??
                                                throw new ArgumentNullException(nameof(saveSurveyResultPubSubClient));
            this.saveSurveyStatusPubSubClient = saveSurveyStatusPubSubClient ??
                                                throw new ArgumentNullException(nameof(saveSurveyStatusPubSubClient));

            this.createMailPubSubClient = createMailPubSubClient ??
                                          throw new ArgumentNullException(nameof(createMailPubSubClient));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(IInitializeSurveyMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var internalSurveyId = Guid.NewGuid().ToString();

            await this.saveSurveyPubSubClient.PublishAsync(
                new SaveSurveyMessage(message.Survey, internalSurveyId, message.ProcessId));

            await this.saveSurveyStatusPubSubClient.PublishAsync(
                new SaveSurveyStatusMessage(message.ProcessId, new SurveyStatus(internalSurveyId, Status.Created)));

            foreach (var surveyParticipant in message.Survey.Participants)
            {
                var saveSurveyResultMessage = new SaveSurveyResultMessage(
                    message.ProcessId,
                    new SurveyResult(
                        internalSurveyId,
                        surveyParticipant.Id,
                        true,
                        surveyParticipant.QuestionReferences));
                await this.saveSurveyResultPubSubClient.PublishAsync(saveSurveyResultMessage);
            }

            var json = JsonConvert.SerializeObject(
                new CreateMailMessage(
                    message.ProcessId,
                    MailType.RequestForParticipation,
                    new RequestForParticipation(internalSurveyId, message.Survey)));
            await this.createMailPubSubClient.PublishAsync(
                new CreateMailMessage(
                    message.ProcessId,
                    MailType.RequestForParticipation,
                    new RequestForParticipation(internalSurveyId, message.Survey)));
        }
    }
}

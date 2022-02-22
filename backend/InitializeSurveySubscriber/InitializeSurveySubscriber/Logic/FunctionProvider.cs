namespace InitializeSurveySubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts;

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
        private readonly IPubSub saveSurveyPubSub;

        /// <summary>
        ///     Access the pub/sub client for saving survey results.
        /// </summary>
        private readonly IPubSub saveSurveyResultPubSub;

        /// <summary>
        ///     Access the pub/sub client for saving survey status updates.
        /// </summary>
        private readonly IPubSub saveSurveyStatusPubSub;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="configuration">Access to the application settings.</param>
        /// <param name="saveSurveyPubSub">Access the pub/sub client for saving surveys.</param>
        /// <param name="saveSurveyResultPubSub">Access the pub/sub client for saving survey results.</param>
        /// <param name="saveSurveyStatusPubSub">Access the pub/sub client for saving survey status updates.</param>
        public FunctionProvider(
            IFunctionConfiguration configuration,
            IPubSub saveSurveyPubSub,
            IPubSub saveSurveyResultPubSub,
            IPubSub saveSurveyStatusPubSub
        )
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.saveSurveyPubSub = saveSurveyPubSub ?? throw new ArgumentNullException(nameof(saveSurveyPubSub));
            this.saveSurveyResultPubSub = saveSurveyResultPubSub
                                          ?? throw new ArgumentNullException(nameof(saveSurveyResultPubSub));
            this.saveSurveyStatusPubSub = saveSurveyStatusPubSub
                                          ?? throw new ArgumentNullException(nameof(saveSurveyStatusPubSub));
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

            await this.saveSurveyStatusPubSub.PublishAsync(
                new SaveSurveyStatusMessage(
                    message.ProcessId,
                    new SurveyStatus(internalSurveyId, null, Status.Created)));

            foreach (var surveyParticipant in message.Survey.Participants)
            {
                var saveSurveyResultMessage = new SaveSurveyResultMessage(
                    message.ProcessId,
                    new SurveyResult(
                        internalSurveyId,
                        surveyParticipant.Id,
                        true,
                        surveyParticipant.QuestionReferences));
                await this.saveSurveyResultPubSub.PublishAsync(saveSurveyResultMessage);
            }
        }
    }
}

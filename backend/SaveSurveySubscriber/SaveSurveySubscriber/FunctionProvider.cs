namespace SaveSurveySubscriber
{
    using System;
    using System.Threading.Tasks;
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
    public class FunctionProvider : PubSubProvider<ISaveSurveyMessage, Function>
    {
        /// <summary>
        ///     Publisher client for creating emails.
        /// </summary>
        private readonly ICreateMailPubSubClient createMailPubSubClient;

        /// <summary>
        ///     Access to the survey database.
        /// </summary>
        private readonly ISurveyDatabase database;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the survey database.</param>
        /// <param name="createMailPubSubClient">Publisher client for creating emails.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISurveyDatabase database,
            ICreateMailPubSubClient createMailPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.createMailPubSubClient = createMailPubSubClient ??
                                          throw new ArgumentNullException(nameof(createMailPubSubClient));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ISaveSurveyMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var survey = new Survey(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                message.Survey.ParentDocumentId,
                message.Survey.Name,
                message.Survey.Info,
                message.Survey.Link,
                message.Survey.Organizer,
                message.Survey.Participants,
                message.Survey.Questions);
            await this.database.InsertAsync(survey.DocumentId, survey);
            await this.createMailPubSubClient.PublishAsync(
                new CreateMailMessage(message.ProcessId, MailType.RequestForParticipation, survey));
        }
    }
}

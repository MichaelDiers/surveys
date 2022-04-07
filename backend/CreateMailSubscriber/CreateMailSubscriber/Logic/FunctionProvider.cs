namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CreateMailSubscriber.Contracts;
    using CreateMailSubscriber.Model;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ICreateMailMessage, Function>
    {
        /// <summary>
        ///     The application configuration.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Read email templates from the database.
        /// </summary>
        private readonly IEmailTemplateReadOnlyDatabase database;

        /// <summary>
        ///     Access the pub/sub client for sending emails.
        /// </summary>
        private readonly ISendMailPubSubClient sendMailPubSubClient;

        /// <summary>
        ///     Access surveys database.
        /// </summary>
        private readonly ISurveyReadOnlyDatabase surveyReadOnlyDatabase;

        /// <summary>
        ///     Access survey results database.
        /// </summary>
        private readonly ISurveyResultReadOnlyDatabase surveyResultReadOnlyDatabase;


        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="sendMailPubSubClient">Access the pub/sub client for sending emails.</param>
        /// <param name="database">Access the templates database.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="surveyReadOnlyDatabase">Access surveys database.</param>
        /// <param name="surveyResultReadOnlyDatabase">Access survey results database.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISendMailPubSubClient sendMailPubSubClient,
            IEmailTemplateReadOnlyDatabase database,
            IFunctionConfiguration configuration,
            ISurveyReadOnlyDatabase surveyReadOnlyDatabase,
            ISurveyResultReadOnlyDatabase surveyResultReadOnlyDatabase
        )
            : base(logger)
        {
            this.sendMailPubSubClient =
                sendMailPubSubClient ?? throw new ArgumentNullException(nameof(sendMailPubSubClient));
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.configuration = configuration;
            this.surveyReadOnlyDatabase = surveyReadOnlyDatabase;
            this.surveyResultReadOnlyDatabase = surveyResultReadOnlyDatabase;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ICreateMailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var template = await this.database.ReadByDocumentIdAsync(message.MailType.ToString());
            if (template == null)
            {
                throw new ArgumentException($"Unknown template {message.MailType}", nameof(message.MailType));
            }

            switch (message.MailType)
            {
                case MailType.RequestForParticipation:
                    await this.SendRequestForParticipationAsync(message, new RequestForParticipationTemplate(template));
                    break;
                case MailType.ThankYou:
                    await this.SendThankYouMail(message, new ThankYouTemplate(template));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(message.MailType),
                        message.MailType,
                        "Unsupported value.");
            }
        }

        /// <summary>
        ///     Send an email to the participants of the survey.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <param name="template">The email template.</param>
        /// <returns>A <see cref="Task" />.</returns>
        private async Task SendRequestForParticipationAsync(
            ICreateMailMessage message,
            RequestForParticipationTemplate template
        )
        {
            var survey = message.RequestForParticipation?.Survey;

            var subject = template.Subject(survey.Name);
            foreach (var participant in message.RequestForParticipation.Survey.Participants)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(participant.Email, participant.Name)},
                    new Recipient(survey.Organizer.Email, survey.Organizer.Name),
                    subject,
                    new Body(
                        template.BodyHtml(
                            participant.Name,
                            survey.Name,
                            string.Format(
                                this.configuration.FrondEndUrlFormat,
                                message.RequestForParticipation.InternalSurveyId,
                                participant.Id),
                            survey.Organizer.Name),
                        template.BodyPlain(
                            participant.Name,
                            survey.Name,
                            string.Format(
                                this.configuration.FrondEndUrlFormat,
                                message.RequestForParticipation.InternalSurveyId,
                                participant.Id),
                            survey.Organizer.Name)));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }

        /// <summary>
        ///     Create thank you mail.
        /// </summary>
        /// <param name="message">The incoming message.</param>
        /// <param name="template">The email template.</param>
        /// <returns>A <see cref="Task" />.</returns>
        private async Task SendThankYouMail(ICreateMailMessage message, ThankYouTemplate template)
        {
            if (string.IsNullOrWhiteSpace(message.DocumentId))
            {
                throw new ArgumentException($"{nameof(message.DocumentId)} is not set!", nameof(message));
            }

            var surveyResult = await this.surveyResultReadOnlyDatabase.ReadByDocumentIdAsync(message.DocumentId);
            if (surveyResult == null)
            {
                await this.LogErrorAsync(
                    new Exception(),
                    $"No survey result found for document id {message.DocumentId}");
                return;
            }

            var survey = await this.surveyReadOnlyDatabase.ReadByDocumentIdAsync(surveyResult.InternalSurveyId);
            if (survey == null)
            {
                await this.LogErrorAsync(
                    new Exception(),
                    $"No survey found for document id {surveyResult.InternalSurveyId}");
                return;
            }

            var participant = survey.Participants.FirstOrDefault(p => p.Id == surveyResult.ParticipantId);
            if (participant == null)
            {
                await this.LogErrorAsync(
                    new Exception(),
                    $"No participant ({surveyResult.ParticipantId}) found for survey {surveyResult.InternalSurveyId}");
                return;
            }

            var subject = template.Subject(survey.Name);
            var results = survey.Questions.OrderBy(q => q.Order)
                .Select(
                    q =>
                    {
                        var surveyResultChoice = surveyResult.Results.First(src => src.QuestionId == q.Id);
                        return (q.Text, q.Choices.First(c => c.Id == surveyResultChoice.ChoiceId).Answer);
                    })
                .ToArray();
            var resultPlain =
                template.ResultListPlain(results.Select(tuple => template.ResultPlain(tuple.Text, tuple.Answer)));
            var resultHtml =
                template.ResultListHtml(results.Select(tuple => template.ResultHtml(tuple.Text, tuple.Answer)));

            var sendMailMessage = new SendMailMessage(
                message.ProcessId,
                new[] {new Recipient(participant.Email, participant.Name)},
                new Recipient(survey.Organizer.Email, survey.Organizer.Name),
                subject,
                new Body(
                    template.BodyHtml(
                        participant.Name,
                        survey.Name,
                        string.Format(
                            this.configuration.FrondEndUrlFormat,
                            surveyResult.InternalSurveyId,
                            participant.Id),
                        survey.Organizer.Name,
                        resultHtml),
                    template.BodyPlain(
                        participant.Name,
                        survey.Name,
                        string.Format(
                            this.configuration.FrondEndUrlFormat,
                            surveyResult.InternalSurveyId,
                            participant.Id),
                        survey.Organizer.Name,
                        resultPlain)));
            await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
        }
    }
}

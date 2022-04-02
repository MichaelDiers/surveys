namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using CreateMailSubscriber.Contracts;
    using CreateMailSubscriber.Model;
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
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="sendMailPubSubClient">Access the pub/sub client for sending emails.</param>
        /// <param name="database">Access the templates database.</param>
        /// <param name="configuration">The application configuration.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISendMailPubSubClient sendMailPubSubClient,
            IEmailTemplateReadOnlyDatabase database,
            IFunctionConfiguration configuration
        )
            : base(logger)
        {
            this.sendMailPubSubClient =
                sendMailPubSubClient ?? throw new ArgumentNullException(nameof(sendMailPubSubClient));
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.configuration = configuration;
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
                            survey.Organizer.Name)),
                    message.RequestForParticipation.InternalSurveyId,
                    new[] {participant.Id},
                    Status.InvitationMailSentOk,
                    Status.InvitationMailSentFailed);
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }
    }
}

namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using CreateMailSubscriber.Contracts;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
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
        ///     Access the pub/sub client for sending emails.
        /// </summary>
        private readonly ISendMailPubSubClient sendMailPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="sendMailPubSubClient">Access the pub/sub client for sending emails.</param>
        /// <param name="configuration">The application configuration.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISendMailPubSubClient sendMailPubSubClient,
            IFunctionConfiguration configuration
        )
            : base(logger)
        {
            this.sendMailPubSubClient =
                sendMailPubSubClient ?? throw new ArgumentNullException(nameof(sendMailPubSubClient));
            this.configuration = configuration;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ICreateMailMessage message)
        {
            switch (message.MailType)
            {
                case MailType.Undefined:
                    throw new ArgumentException(
                        $"Cannot handle undefined mail type: {message.MailType}",
                        nameof(message.MailType));
                case MailType.RequestForParticipation:
                    await this.HandleRequestForParticipationAsync(message);
                    break;
                case MailType.ThankYou:
                    await this.HandleThankYouAsync(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(message.MailType),
                        message.MailType,
                        "Cannot handle mail type.");
            }

            await Task.CompletedTask;
        }

        /// <summary>
        ///     Handle participation request emails.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>A <see cref="Task" /> that indicates termination.</returns>
        private async Task HandleRequestForParticipationAsync(ICreateMailMessage message)
        {
            if (message.Survey == null)
            {
                throw new ArgumentNullException(nameof(message.Survey));
            }

            foreach (var surveyParticipant in message.Survey.Participants)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(surveyParticipant.Email, surveyParticipant.Name)},
                    new Recipient(message.Survey.Organizer.Email, message.Survey.Name),
                    string.Format(RequestForParticipation.Subject, message.Survey.Name),
                    this.HandleRequestForParticipationBody(message.Survey, surveyParticipant));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }

        /// <summary>
        ///     Create the body for participation request emails.
        /// </summary>
        /// <param name="survey">The survey data.</param>
        /// <param name="participant">The participant data.</param>
        /// <returns>The body of the email.</returns>
        private Body HandleRequestForParticipationBody(ISurvey survey, IParticipant participant)
        {
            var frontEndUrl = string.Format(this.configuration.FrondEndUrlFormat, survey.DocumentId, participant.Id);
            return new Body(
                string.Format(
                    RequestForParticipation.BodyHtml,
                    participant.Name,
                    survey.Name,
                    frontEndUrl,
                    survey.Organizer.Name),
                string.Format(
                    RequestForParticipation.BodyText,
                    participant.Name,
                    survey.Name,
                    frontEndUrl,
                    survey.Organizer.Name));
        }

        /// <summary>
        ///     Handle thank you mails.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>A <see cref="Task" /> that indicates termination.</returns>
        private async Task HandleThankYouAsync(ICreateMailMessage message)
        {
            await Task.CompletedTask;
        }
    }
}

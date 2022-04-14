namespace CreateMailSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CreateMailSubscriber.Contracts;
    using Md.Common.Logic;
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
            if (message.SurveyResult == null)
            {
                await this.LogErrorAsync(
                    new ArgumentNullException(nameof(message.SurveyResult)),
                    "Survey result is not set.");
                return;
            }

            var participant = message.Survey.Participants.FirstOrDefault(
                participant => participant.Id == message.SurveyResult.ParticipantId);
            if (participant == null)
            {
                await this.LogErrorAsync(
                    new Exception("Survey and survey result do not match. Participant not found."),
                    Serializer.SerializeObject(message));
                return;
            }

            var sendMailMessage = new SendMailMessage(
                message.ProcessId,
                new[] {new Recipient(participant.Email, participant.Email)},
                new Recipient(message.Survey.Organizer.Email, message.Survey.Organizer.Name),
                string.Format(ThankYou.Subject, message.Survey.Name),
                this.HandleThankYouBody(message.Survey, participant, message.SurveyResult));
            await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
        }

        private Body HandleThankYouBody(ISurvey survey, IParticipant participant, ISurveyResult surveyResult)
        {
            var htmlBuilder = new StringBuilder();
            var textBuilder = new StringBuilder();
            foreach (var question in survey.Questions.OrderBy(question => question.Order))
            {
                var questionReference = surveyResult.Results.FirstOrDefault(qr => qr.QuestionId == question.Id);
                if (questionReference == null)
                {
                    throw new Exception(
                        $"Survey result does not contain question id {question.Id}: {Serializer.SerializeObject(survey)} - {Serializer.SerializeObject(surveyResult)}");
                }

                var answer = question.Choices.FirstOrDefault(choice => choice.Id == questionReference.ChoiceId);
                if (answer == null)
                {
                    throw new Exception(
                        $"Question {question.Id} does not include choice {questionReference.ChoiceId} - {Serializer.SerializeObject(survey)} - {Serializer.SerializeObject(surveyResult)}");
                }

                htmlBuilder.AppendFormat(ThankYou.BodyHtmlResult, question.Text, answer.Answer);
                textBuilder.AppendFormat(ThankYou.BodyTextResult, question.Text, answer.Answer);
            }

            var htmlResultList = string.Format(ThankYou.BodyHtmlResultList, htmlBuilder);
            var textResultList = string.Format(ThankYou.BodyTextResultList, textBuilder);

            var surveyLink = string.Format(this.configuration.FrondEndUrlFormat, survey.DocumentId, participant.Id);
            var html = string.Format(
                ThankYou.BodyHtml,
                participant.Name,
                survey.Name,
                surveyLink,
                survey.Organizer.Name,
                htmlResultList);

            var text = string.Format(
                ThankYou.BodyText,
                participant.Name,
                survey.Name,
                surveyLink,
                survey.Organizer.Name,
                textResultList);

            return new Body(html, text);
        }
    }
}

namespace MailerService.Logic
{
    using System;
    using System.Threading.Tasks;
    using MailerService.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.Extensions.Logging;
    using MimeKit;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;

    /// <summary>
    ///     Provider for sending email messages for surveys.
    /// </summary>
    public class MailerProvider : PubSubProvider<ISendMailMessage, MailerFunction>
    {
        /// <summary>
        ///     The application configuration.
        /// </summary>
        private readonly IMailerServiceConfiguration configuration;

        /// <summary>
        ///     Mail for sending emails via smtp.
        /// </summary>
        private readonly IMailerSmtpClient mailerSmtpClient;

        /// <summary>
        ///     Converter for <see cref="ISendMailMessage" /> to <see cref="MimeMessage" />.
        /// </summary>
        private readonly IMessageConverter messageConverter;

        /// <summary>
        ///     Access the Pub/Sub client.
        /// </summary>
        private readonly IPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="MailerProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="messageConverter">Converter for <see cref="ISendMailMessage" /> to <see cref="MimeMessage" />.</param>
        /// <param name="mailerSmtpClient">Sends mails via smtp.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="pubSubClient">Access the Pub/Sub client.</param>
        public MailerProvider(
            ILogger<MailerFunction> logger,
            IMessageConverter messageConverter,
            IMailerSmtpClient mailerSmtpClient,
            IMailerServiceConfiguration configuration,
            IPubSubClient pubSubClient
        )
            : base(logger)
        {
            this.messageConverter = messageConverter ?? throw new ArgumentNullException(nameof(messageConverter));
            this.mailerSmtpClient = mailerSmtpClient ?? throw new ArgumentNullException(nameof(mailerSmtpClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.pubSubClient = pubSubClient ?? throw new ArgumentNullException(nameof(pubSubClient));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        protected override async Task HandleMessageAsync(ISendMailMessage message)
        {
            var mimeMessageFrom = new[]
            {
                new MailboxAddress(
                    this.configuration.MailboxAddressFrom.Name,
                    this.configuration.MailboxAddressFrom.Email)
            };

            var email = this.messageConverter.ToMimeMessage(
                message,
                mimeMessageFrom,
                this.configuration.TemplateNewline);

            var success = false;
            try
            {
                await this.mailerSmtpClient.SendAsync(email, this.configuration.Smtp);
                success = true;
            }
            finally
            {
                var status = success ? message.StatusOk : message.StatusFailed;
                foreach (var participantId in message.ParticipantIds)
                {
                    var saveSurveyStatusMessage = new SaveSurveyStatusMessage(
                        message.ProcessId,
                        new SurveyStatus(message.SurveyId, participantId, status));
                    await this.pubSubClient.PublishAsync(saveSurveyStatusMessage);
                }
            }
        }
    }
}

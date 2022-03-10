namespace Surveys.Common.Messages
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Describes a send mail message.
    /// </summary>
    public class SendMailMessage : Message
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SendMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="recipients">The recipients of the email.</param>
        /// <param name="replyTo">The reply to address of the email.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The email body for html and plain text.</param>
        /// <param name="surveyId">The survey id.</param>
        /// <param name="participantIds">The ids of the participants.</param>
        /// <param name="statusOk">The status if the email is sent.</param>
        /// <param name="statusFailed">The status if the process failed.</param>
        [JsonConstructor]
        public SendMailMessage(
            string processId,
            IEnumerable<Recipient> recipients,
            Recipient replyTo,
            string subject,
            Body body,
            string surveyId,
            IEnumerable<string> participantIds,
            string statusOk,
            string statusFailed
        )
            : this(
                processId,
                recipients,
                replyTo,
                subject,
                body as IBody,
                surveyId,
                participantIds,
                statusOk,
                statusFailed)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SendMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="recipients">The recipients of the email.</param>
        /// <param name="replyTo">The reply to address of the email.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The email body for html and plain text.</param>
        /// <param name="surveyId">The survey id.</param>
        /// <param name="participantIds">The ids of the participants.</param>
        /// <param name="statusOk">The status if the email is sent.</param>
        /// <param name="statusFailed">The status if the process failed.</param>
        public SendMailMessage(
            string processId,
            IEnumerable<IRecipient> recipients,
            IRecipient replyTo,
            string subject,
            IBody body,
            string surveyId,
            IEnumerable<string> participantIds,
            string statusOk,
            string statusFailed
        )
            : base(processId)
        {
            this.Recipients = recipients;
            this.ReplyTo = replyTo;
            this.Subject = subject.ValidateIsNotNullOrWhitespace(nameof(subject));
            this.Body = body;
            this.SurveyId = surveyId.ValidateIsNotNullOrWhitespace(nameof(surveyId));
            this.ParticipantIds = participantIds;
            this.StatusOk = statusOk.ValidateIsNotNullOrWhitespace(nameof(statusOk));
            this.StatusFailed = statusFailed.ValidateIsNotNullOrWhitespace(nameof(statusFailed));
        }

        /// <summary>
        ///     Gets or ses the body of the message.
        /// </summary>
        [JsonProperty("text", Required = Required.Always, Order = 14)]
        public IBody Body { get; }

        /// <summary>
        ///     Gets or sets the id of the recipients or participants.
        /// </summary>
        [JsonProperty("participantIds", Required = Required.Always, Order = 16)]
        public IEnumerable<string> ParticipantIds { get; }

        /// <summary>
        ///     Gets or sets the recipients of the email.
        /// </summary>
        [JsonProperty("recipients", Required = Required.Always, Order = 11)]
        public IEnumerable<IRecipient> Recipients { get; }

        /// <summary>
        ///     Gets or sets the reply to email address.
        /// </summary>
        [JsonProperty("replyTo", Required = Required.Always, Order = 12)]
        public IRecipient ReplyTo { get; }

        /// <summary>
        ///     Gets or sets the status that indicates failure.
        /// </summary>
        [JsonProperty("statusFailed", Required = Required.Always, Order = 18)]
        public string StatusFailed { get; }

        /// <summary>
        ///     Gets or sets the status that indicates success.
        /// </summary>
        [JsonProperty("statusOk", Required = Required.Always, Order = 17)]
        public string StatusOk { get; }

        /// <summary>
        ///     Gets or sets the subject of the email.
        /// </summary>
        [JsonProperty("subject", Required = Required.Always, Order = 13)]
        public string Subject { get; }

        /// <summary>
        ///     Gets or sets the id of the survey.
        /// </summary>
        [JsonProperty("surveyId", Required = Required.Always, Order = 15)]
        public string SurveyId { get; }
    }
}

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
    public class SendMailMessage : Message, ISendMailMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SendMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="recipients">The recipients of the email.</param>
        /// <param name="replyTo">The reply to address of the email.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The email body for html and plain text.</param>
        [JsonConstructor]
        public SendMailMessage(
            string processId,
            IEnumerable<Recipient> recipients,
            Recipient replyTo,
            string subject,
            Body body
        )
            : this(
                processId,
                recipients,
                replyTo as IRecipient,
                subject,
                body)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SendMailMessage" />.
        /// </summary>
        /// <param name="processId"> The global process id.</param>
        /// <param name="recipients"> The recipients of the email.</param>
        /// <param name="replyTo"> The reply to address of the email.</param>
        /// <param name="subject"> The subject of the email.</param>
        /// <param name="body"> The email body for html and plain text.</param>
        public SendMailMessage(
            string processId,
            IEnumerable<IRecipient> recipients,
            IRecipient replyTo,
            string subject,
            Body body
        )
            : base(processId)
        {
            this.Recipients = recipients;
            this.ReplyTo = replyTo;
            this.Subject = subject.ValidateIsNotNullOrWhitespace(nameof(subject));
            this.Body = body;
        }

        /// <summary>
        ///     Gets or ses the body of the message.
        /// </summary>
        [JsonProperty("text", Required = Required.Always, Order = 14)]
        public Body Body { get; }


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
        ///     Gets or sets the subject of the email.
        /// </summary>
        [JsonProperty("subject", Required = Required.Always, Order = 13)]
        public string Subject { get; }
    }
}

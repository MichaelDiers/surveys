namespace Surveys.Common.Messages
{
    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Message for creating emails.
    /// </summary>
    public class CreateMailMessage : Message, ICreateMailMessage
    {
        /// <summary>
        ///     Creates an instance of <see cref="CreateMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="mailType">The type of the email.</param>
        /// <param name="requestForParticipation">The data for creating a request of participation.</param>
        [JsonConstructor]
        public CreateMailMessage(string processId, MailType mailType, RequestForParticipation? requestForParticipation)
            : this(processId, mailType, requestForParticipation as IRequestForParticipation)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="CreateMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="mailType">The type of the email.</param>
        /// <param name="requestForParticipation">The data for creating a request of participation.</param>
        public CreateMailMessage(string processId, MailType mailType, IRequestForParticipation? requestForParticipation)
            : base(processId)
        {
            if (!Enum.IsDefined(typeof(MailType), mailType) || mailType == MailType.Undefined)
            {
                throw new InvalidEnumArgumentException(nameof(mailType), (int) mailType, typeof(MailType));
            }

            if (mailType == MailType.RequestForParticipation && requestForParticipation == null)
            {
                throw new ArgumentException(
                    $"Value cannot be null for specified email type: {mailType}",
                    nameof(requestForParticipation));
            }

            this.MailType = mailType;
            this.RequestForParticipation = requestForParticipation;
        }

        /// <summary>
        ///     Gets a value that specifies the type of the email.
        /// </summary>
        [JsonProperty("mailType", Required = Required.Always, Order = 11)]
        public MailType MailType { get; }

        /// <summary>
        ///     Gets the data for <see cref="Surveys.Common.Contracts.Messages.MailType.RequestForParticipation" />
        /// </summary>
        [JsonProperty("requestForParticipation", Required = Required.Default, Order = 12)]
        public IRequestForParticipation? RequestForParticipation { get; }
    }
}

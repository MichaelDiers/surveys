namespace Surveys.Common.Messages
{
    using System;
    using System.ComponentModel;
    using Md.Common.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Models;

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
        /// <param name="survey">The survey data.</param>
        [JsonConstructor]
        public CreateMailMessage(string processId, MailType mailType, Survey? survey)
            : this(processId, mailType, survey as ISurvey)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="CreateMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="mailType">The type of the email.</param>
        /// <param name="survey">The survey data.</param>
        public CreateMailMessage(string processId, MailType mailType, ISurvey? survey)
            : base(processId)
        {
            if (!Enum.IsDefined(typeof(MailType), mailType) || mailType == MailType.Undefined)
            {
                throw new InvalidEnumArgumentException(nameof(mailType), (int) mailType, typeof(MailType));
            }

            this.MailType = mailType;
            this.Survey = survey;
        }


        /// <summary>
        ///     Gets a value that specifies the type of the email.
        /// </summary>
        [JsonProperty("mailType", Required = Required.Always, Order = 10)]
        public MailType MailType { get; }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 11)]
        public ISurvey? Survey { get; }
    }
}

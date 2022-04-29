namespace Surveys.Common.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
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
        /// <param name="surveyResult">The optional survey result for thank you mails.</param>
        /// <param name="reminderParticipantIds">The participant ids for that a reminder is sent.</param>
        [JsonConstructor]
        public CreateMailMessage(
            string processId,
            MailType mailType,
            Survey survey,
            SurveyResult? surveyResult,
            IEnumerable<string> reminderParticipantIds
        )
            : this(
                processId,
                mailType,
                survey,
                surveyResult as ISurveyResult,
                reminderParticipantIds)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="CreateMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="mailType">The type of the email.</param>
        /// <param name="survey">The survey data.</param>
        public CreateMailMessage(string processId, MailType mailType, ISurvey survey)
            : this(
                processId,
                mailType,
                survey,
                null,
                Enumerable.Empty<string>())
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="CreateMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="mailType">The type of the email.</param>
        /// <param name="survey">The survey data.</param>
        /// <param name="surveyResult">The result for that a mail is created.</param>
        /// <param name="reminderParticipantIds">The participant ids for that a reminder is sent.</param>
        public CreateMailMessage(
            string processId,
            MailType mailType,
            ISurvey survey,
            ISurveyResult? surveyResult,
            IEnumerable<string> reminderParticipantIds
        )
            : base(processId)
        {
            if (!Enum.IsDefined(typeof(MailType), mailType) || mailType == MailType.Undefined)
            {
                throw new InvalidEnumArgumentException(nameof(mailType), (int) mailType, typeof(MailType));
            }

            this.MailType = mailType;
            this.Survey = survey;
            this.SurveyResult = surveyResult;
            this.ReminderParticipantIds = reminderParticipantIds;
        }

        /// <summary>
        ///     Gets a value that specifies the type of the email.
        /// </summary>
        [JsonProperty("mailType", Required = Required.Always, Order = 10)]
        public MailType MailType { get; }

        /// <summary>
        ///     Gets the participant ids for that a reminder is sent.
        /// </summary>
        [JsonProperty("reminderParticipantIds", Required = Required.AllowNull, Order = 13)]
        public IEnumerable<string> ReminderParticipantIds { get; }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 11)]
        public ISurvey Survey { get; }

        /// <summary>
        ///     Gets the optional survey result.
        /// </summary>
        [JsonProperty("surveyResult", Required = Required.AllowNull, Order = 12)]
        public ISurveyResult? SurveyResult { get; }
    }
}

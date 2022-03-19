namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public class SurveyStatus : ToDictionaryConverter, ISurveyStatus
    {
        /// <summary>
        ///     Json name of property <see cref="InternalSurveyId" />.
        /// </summary>
        public const string InternalSurveyIdName = "internalSurveyId";

        /// <summary>
        ///     Json name of property <see cref="ParticipantId" />.
        /// </summary>
        private const string ParticipantIdName = "participantId";

        /// <summary>
        ///     Json name of property <see cref="Status" />.
        /// </summary>
        private const string StatusName = "status";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatus" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal survey id.</param>
        /// <param name="status">The new status.</param>
        public SurveyStatus(string internalSurveyId, Status status)
            : this(internalSurveyId, string.Empty, status)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatus" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal survey id.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="status">The new status.</param>
        [JsonConstructor]
        public SurveyStatus(string internalSurveyId, string participantId, Status status)
        {
            if (!string.IsNullOrWhiteSpace(participantId))
            {
                participantId.ValidateIsAGuid(nameof(participantId));
            }

            if (status == Status.None)
            {
                throw new ArgumentException("Invalid value Status.None", nameof(status));
            }

            this.InternalSurveyId = internalSurveyId.ValidateIsAGuid(nameof(internalSurveyId));
            this.ParticipantId = participantId;
            this.Status = status;
        }


        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        [JsonProperty(InternalSurveyIdName, Required = Required.Always, Order = 1)]
        public string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        [JsonProperty(ParticipantIdName, Required = Required.AllowNull, Order = 2)]
        public string ParticipantId { get; }

        /// <summary>
        ///     Gets the status of the survey.
        /// </summary>
        [JsonProperty(StatusName, Required = Required.Always, Order = 3)]
        public Status Status { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(InternalSurveyIdName, this.InternalSurveyId);
            dictionary.Add(ParticipantIdName, this.ParticipantId);
            dictionary.Add(StatusName, this.Status.ToString());
            return dictionary;
        }
    }
}

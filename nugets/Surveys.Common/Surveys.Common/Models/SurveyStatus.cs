namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public class SurveyStatus : ISurveyStatus
    {
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
            internalSurveyId.ThrowExceptionIfGuidIsInvalid();
            if (!string.IsNullOrWhiteSpace(participantId))
            {
                participantId.ThrowExceptionIfGuidIsInvalid();
            }

            if (status == Status.None)
            {
                throw new ArgumentException("Invalid value Status.None", nameof(status));
            }

            this.InternalSurveyId = internalSurveyId;
            this.ParticipantId = participantId;
            this.Status = status;
        }

        /// <summary>
        ///     Add the object to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        public void AddToDictionary(Dictionary<string, object> document)
        {
            document.Add(nameof(this.InternalSurveyId).FirstCharacterToLower(), this.InternalSurveyId);
            document.Add(nameof(this.ParticipantId).FirstCharacterToLower(), this.ParticipantId);
            document.Add(nameof(this.Status).FirstCharacterToLower(), this.Status.ToString());
        }

        /// <summary>
        ///     Convert the object values to a dictionary.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public Dictionary<string, object> ToDictionary()
        {
            var document = new Dictionary<string, object>();
            this.AddToDictionary(document);
            return document;
        }

        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        [JsonProperty("internalSurveyId", Required = Required.Always, Order = 1)]
        public string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        [JsonProperty("participantId", Required = Required.AllowNull, Order = 2)]
        public string ParticipantId { get; }

        /// <summary>
        ///     Gets the status of the survey.
        /// </summary>
        [JsonProperty("status", Required = Required.Always, Order = 3)]
        public Status Status { get; }
    }
}

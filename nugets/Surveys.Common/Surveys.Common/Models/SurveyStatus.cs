namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public class SurveyStatus : DatabaseObject, ISurveyStatus
    {
        /// <summary>
        ///     Json name of property <see cref="ParticipantId" />.
        /// </summary>
        public const string ParticipantIdName = "participantId";

        /// <summary>
        ///     Json name of property <see cref="Status" />.
        /// </summary>
        public const string StatusName = "status";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatus" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="status">The new status.</param>
        public SurveyStatus(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            Status status
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                string.Empty,
                status)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatus" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="status">The new status.</param>
        [JsonConstructor]
        public SurveyStatus(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string participantId,
            Status status
        )
            : base(documentId, created, parentDocumentId)
        {
            if (!string.IsNullOrWhiteSpace(participantId))
            {
                participantId.ValidateIsAGuid(nameof(participantId));
            }

            if (status == Status.None)
            {
                throw new ArgumentException("Invalid value Status.None", nameof(status));
            }

            this.ParticipantId = participantId;
            this.Status = status;
        }

        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        [JsonProperty(SurveyStatus.ParticipantIdName, Required = Required.AllowNull, Order = 2)]
        public string ParticipantId { get; }

        /// <summary>
        ///     Gets the status of the survey.
        /// </summary>
        [JsonProperty(SurveyStatus.StatusName, Required = Required.Always, Order = 3)]
        public Status Status { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(SurveyStatus.ParticipantIdName, this.ParticipantId);
            dictionary.Add(SurveyStatus.StatusName, this.Status.ToString());
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="SurveyStatus" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="SurveyStatus" />.</returns>
        public new static SurveyStatus FromDictionary(IDictionary<string, object> dictionary)
        {
            var value = DatabaseObject.FromDictionary(dictionary);
            var participantId = dictionary.GetString(SurveyStatus.ParticipantIdName, string.Empty);
            var status = dictionary.GetEnumValue<Status>(SurveyStatus.StatusName);

            return new SurveyStatus(
                value.DocumentId,
                value.Created,
                value.ParentDocumentId,
                participantId,
                status);
        }
    }
}

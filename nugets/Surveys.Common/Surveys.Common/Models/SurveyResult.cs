namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Describes a survey result.
    /// </summary>
    public class SurveyResult : DatabaseObject, ISurveyResult
    {
        /// <summary>
        ///     Json name of property <see cref="IsSuggested" />.
        /// </summary>
        public const string IsSuggestedName = "isSuggested";

        /// <summary>
        ///     Json name of property <see cref="ParticipantId" />.
        /// </summary>
        private const string ParticipantIdName = "participantId";

        /// <summary>
        ///     Json name of property <see cref="Results" />.
        /// </summary>
        private const string ResultsName = "results";

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultMessage" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="isSuggested">A value that indicates if the result is a suggested result or a real survey result.</param>
        /// <param name="results">The survey results.</param>
        [JsonConstructor]
        public SurveyResult(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string participantId,
            bool isSuggested,
            IEnumerable<QuestionReference> results
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                participantId,
                isSuggested,
                results as IEnumerable<IQuestionReference>)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultMessage" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="isSuggested">A value that indicates if the result is a suggested result or a real survey result.</param>
        /// <param name="results">The survey results.</param>
        public SurveyResult(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string participantId,
            bool isSuggested,
            IEnumerable<IQuestionReference> results
        )
            : base(documentId, created, parentDocumentId)
        {
            this.ParticipantId = participantId.ValidateIsAGuid(nameof(participantId));
            this.IsSuggested = isSuggested;
            this.Results = results ?? throw new ArgumentNullException(nameof(results));
        }

        /// <summary>
        ///     Gets a value that indicates if the result is a suggested result or a real survey result.
        /// </summary>
        [JsonProperty("isSuggested", Required = Required.Always, Order = 13)]
        public bool IsSuggested { get; }

        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        [JsonProperty("participantId", Required = Required.Always, Order = 12)]
        public string ParticipantId { get; }

        /// <summary>
        ///     Gets the survey results.
        /// </summary>
        [JsonProperty("results", Required = Required.Always, Order = 14)]
        public IEnumerable<IQuestionReference> Results { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(SurveyResult.ParticipantIdName, this.ParticipantId);
            dictionary.Add(SurveyResult.IsSuggestedName, this.IsSuggested);
            dictionary.Add(SurveyResult.ResultsName, this.Results.Select(r => r.ToDictionary()));
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="SurveyResult" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="SurveyResult" />.</returns>
        public new static SurveyResult FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var participantId = dictionary.GetString(SurveyResult.ParticipantIdName);
            var isSuggested = dictionary.GetBool(SurveyResult.IsSuggestedName);
            var results = dictionary.GetDictionaries(SurveyResult.ResultsName)
                .Select(QuestionReference.FromDictionary)
                .ToArray();

            return new SurveyResult(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                participantId,
                isSuggested,
                results);
        }
    }
}

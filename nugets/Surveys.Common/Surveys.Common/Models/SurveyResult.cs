namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;
    using Surveys.Common.Messages;

    /// <summary>
    ///     Describes a survey result.
    /// </summary>
    public class SurveyResult : ISurveyResult
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultMessage" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal id of the survey.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="isSuggested">A value that indicates if the result is a suggested result or a real survey result.</param>
        /// <param name="results">The survey results.</param>
        [JsonConstructor]
        public SurveyResult(
            string internalSurveyId,
            string participantId,
            bool isSuggested,
            IEnumerable<QuestionReference> results
        )
            : this(
                internalSurveyId,
                participantId,
                isSuggested,
                results as IEnumerable<IQuestionReference>)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyResultMessage" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal id of the survey.</param>
        /// <param name="participantId">The id of the participant.</param>
        /// <param name="isSuggested">A value that indicates if the result is a suggested result or a real survey result.</param>
        /// <param name="results">The survey results.</param>
        public SurveyResult(
            string internalSurveyId,
            string participantId,
            bool isSuggested,
            IEnumerable<IQuestionReference> results
        )
        {
            if (string.IsNullOrWhiteSpace(internalSurveyId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(internalSurveyId));
            }

            if (string.IsNullOrWhiteSpace(participantId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(participantId));
            }

            internalSurveyId.ThrowExceptionIfGuidIsInvalid();
            participantId.ThrowExceptionIfGuidIsInvalid();

            this.InternalSurveyId = internalSurveyId;
            this.ParticipantId = participantId;
            this.IsSuggested = isSuggested;
            this.Results = results ?? throw new ArgumentNullException(nameof(results));
        }

        /// <summary>
        ///     Add the object to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        public void AddToDictionary(Dictionary<string, object?> document)
        {
            document.Add(nameof(this.InternalSurveyId).FirstCharacterToLower(), this.InternalSurveyId);
            document.Add(nameof(this.ParticipantId).FirstCharacterToLower(), this.ParticipantId);
            document.Add(nameof(this.IsSuggested).FirstCharacterToLower(), this.IsSuggested);
            document.Add(nameof(this.Results).FirstCharacterToLower(), this.Results.Select(r => r.ToDictionary()));
        }

        /// <summary>
        ///     Convert the object values to a dictionary.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public Dictionary<string, object?> ToDictionary()
        {
            var document = new Dictionary<string, object?>();
            this.AddToDictionary(document);
            return document;
        }

        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        [JsonProperty("internalSurveyId", Required = Required.Always, Order = 11)]
        public string InternalSurveyId { get; }

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
    }
}

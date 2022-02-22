namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    /// <summary>
    ///     Describes a suggested answer of a survey question.
    /// </summary>
    public class QuestionReference : IQuestionReference
    {
        /// <summary>
        ///     Creates a new instance of <see cref="QuestionReference" />.
        /// </summary>
        /// <param name="questionId">The id of the referenced question.</param>
        /// <param name="choiceId">The id of the referenced choice.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="questionId" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="questionId" /> is not a guid.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="choiceId" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="choiceId" /> is not a guid.</exception>
        public QuestionReference(string questionId, string choiceId)
        {
            if (string.IsNullOrWhiteSpace(questionId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(questionId));
            }

            if (string.IsNullOrWhiteSpace(choiceId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(choiceId));
            }

            if (!Guid.TryParse(questionId, out var questionGuid) || questionGuid == Guid.Empty)
            {
                throw new ArgumentException("Value is not a valid guid.", nameof(questionId));
            }

            if (!Guid.TryParse(choiceId, out var choiceGuid) || choiceGuid == Guid.Empty)
            {
                throw new ArgumentException("Value is not a valid guid.", nameof(choiceId));
            }

            this.ChoiceId = choiceId;
            this.QuestionId = questionId;
        }

        /// <summary>
        ///     Add the object values to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public void AddToDictionary(Dictionary<string, object?> document)
        {
            document.Add(nameof(this.QuestionId).FirstCharacterToLower(), this.QuestionId);
            document.Add(nameof(this.ChoiceId).FirstCharacterToLower(), this.ChoiceId);
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
        ///     Gets the id of the choice.
        /// </summary>
        [JsonProperty("choiceId", Required = Required.Always, Order = 12)]
        public string ChoiceId { get; }

        /// <summary>
        ///     Gets the id of the question.
        /// </summary>
        [JsonProperty("questionId", Required = Required.Always, Order = 11)]
        public string QuestionId { get; }
    }
}

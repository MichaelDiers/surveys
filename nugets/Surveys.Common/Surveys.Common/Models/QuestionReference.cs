namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes a suggested answer of a survey question.
    /// </summary>
    public class QuestionReference : ToDictionaryConverter, IQuestionReference
    {
        /// <summary>
        ///     Json name of property <see cref="ChoiceId" />.
        /// </summary>
        private const string ChoiceIdName = "choiceId";

        /// <summary>
        ///     Json name of property <see cref="QuestionId" />.
        /// </summary>
        private const string QuestionIdName = "questionId";

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
            this.ChoiceId = choiceId.ValidateIsAGuid(nameof(choiceId));
            this.QuestionId = questionId.ValidateIsAGuid(nameof(questionId));
        }

        /// <summary>
        ///     Gets the id of the choice.
        /// </summary>
        [JsonProperty(ChoiceIdName, Required = Required.Always, Order = 12)]
        public string ChoiceId { get; }

        /// <summary>
        ///     Gets the id of the question.
        /// </summary>
        [JsonProperty(QuestionIdName, Required = Required.Always, Order = 11)]
        public string QuestionId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(QuestionIdName, this.QuestionId);
            dictionary.Add(ChoiceIdName, this.ChoiceId);
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="QuestionReference" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="QuestionReference" />.</returns>
        public static QuestionReference FromDictionary(IDictionary<string, object> dictionary)
        {
            var questionId = dictionary.GetString(QuestionIdName);
            var choiceId = dictionary.GetString(ChoiceIdName);

            return new QuestionReference(questionId, choiceId);
        }
    }
}

namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describe the choice of a question.
    /// </summary>
    public class Choice : Base, IChoice
    {
        /// <summary>
        ///     Json name of property <see cref="Answer" />.
        /// </summary>
        private const string AnswerName = "answer";

        /// <summary>
        ///     Json name of property <see cref="Order" />.
        /// </summary>
        private const string OrderName = "order";

        /// <summary>
        ///     Json name of property <see cref="Selectable" />.
        /// </summary>
        private const string SelectableName = "selectable";

        /// <summary>
        ///     Creates a new instance of <see cref="Choice" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="answer">The answer of the question.</param>
        /// <param name="selectable">Indicates if the answer is a valid answer or an info text.</param>
        /// <param name="order">The sorting order.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        public Choice(
            string id,
            string answer,
            bool selectable,
            int order
        )
            : base(id)
        {
            this.Answer = answer.ValidateIsNotNullOrWhitespace(nameof(answer));
            this.Order = order;
            this.Selectable = selectable;
        }

        /// <summary>
        ///     Gets the answer of a question.
        /// </summary>
        [JsonProperty(Choice.AnswerName, Required = Required.Always, Order = 10)]
        public string Answer { get; }

        /// <summary>
        ///     Indicates if the answer is a valid answer or an info text.
        /// </summary>
        [JsonProperty(Choice.SelectableName, Required = Required.Always, Order = 11)]
        public bool Selectable { get; }

        /// <summary>
        ///     Gets the sorting order.
        /// </summary>
        [JsonProperty(Choice.OrderName, Required = Required.Always, Order = 12)]
        public int Order { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(Choice.AnswerName, this.Answer);
            dictionary.Add(Choice.SelectableName, this.Selectable);
            dictionary.Add(Choice.OrderName, this.Order);
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="Choice" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="Choice" />.</returns>
        public new static Choice FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = Base.FromDictionary(dictionary);
            var answer = dictionary.GetString(Choice.AnswerName);
            var selectable = dictionary.GetBool(Choice.SelectableName);
            var order = dictionary.GetInt(Choice.OrderName);
            return new Choice(
                baseObject.Id,
                answer,
                selectable,
                order);
        }
    }
}

namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    public class Question : Base, IQuestion, ISortable
    {
        /// <summary>
        ///     Json name of property <see cref="Choices" />.
        /// </summary>
        private const string ChoicesName = "choices";

        /// <summary>
        ///     Json name of property <see cref="Order" />.
        /// </summary>
        private const string OrderName = "order";

        /// <summary>
        ///     Json name of property <see cref="Text" />.
        /// </summary>
        private const string QuestionName = "question";

        /// <summary>
        ///     Creates a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="text">The text of the question.</param>
        /// <param name="choices">The possible answers of the question.</param>
        /// <param name="order">Used for sorting questions.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="text" /> is null or whitespace.</exception>
        public Question(
            string id,
            string text,
            IEnumerable<Choice> choices,
            int order
        )
            : base(id)
        {
            this.Text = text.ValidateIsNotNullOrWhitespace(nameof(text));
            this.Choices = choices ?? throw new ArgumentNullException(nameof(choices));
            this.Order = order;
        }

        /// <summary>
        ///     Gets the choices of the question.
        /// </summary>
        [JsonProperty(ChoicesName, Required = Required.Always, Order = 11)]
        public IEnumerable<IChoice> Choices { get; }

        /// <summary>
        ///     Gets the order of the question used for sorting.
        /// </summary>
        [JsonProperty(OrderName, Required = Required.Always, Order = 12)]
        public int Order { get; }

        /// <summary>
        ///     Gets the text of the question.
        /// </summary>
        [JsonProperty(QuestionName, Required = Required.Always, Order = 10)]
        public string Text { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(OrderName, this.Order);
            dictionary.Add(QuestionName, this.Text);
            dictionary.Add(ChoicesName, this.Choices.Select(c => c.ToDictionary()));
            return dictionary;
        }
    }
}

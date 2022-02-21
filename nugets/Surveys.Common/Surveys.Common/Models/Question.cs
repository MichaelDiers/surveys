namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    public class Question : Base, IQuestion, ISortable
    {
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
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(text));
            }

            this.Text = text;
            this.Choices = choices ?? throw new ArgumentNullException(nameof(choices));
            this.Order = order;
        }

        /// <summary>
        ///     Gets the choices of the question.
        /// </summary>
        [JsonProperty("choices", Required = Required.Always, Order = 11)]
        public IEnumerable<IChoice> Choices { get; }

        /// <summary>
        ///     Gets the order of the question used for sorting.
        /// </summary>
        [JsonProperty("order", Required = Required.Always, Order = 12)]
        public int Order { get; }

        /// <summary>
        ///     Gets the text of the question.
        /// </summary>
        [JsonProperty("question", Required = Required.Always, Order = 10)]
        public string Text { get; }
    }
}

namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    /// <summary>
    ///     Describe the choice of a question.
    /// </summary>
    public class Choice : Base, IChoice
    {
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
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(answer));
            }

            this.Answer = answer;
            this.Order = order;
            this.Selectable = selectable;
        }

        /// <summary>
        ///     Add the object values to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public override void AddToDictionary(Dictionary<string, object> document)
        {
            base.AddToDictionary(document);
            document.Add(nameof(this.Answer).FirstCharacterToLower(), this.Answer);
            document.Add(nameof(this.Selectable).FirstCharacterToLower(), this.Selectable);
            document.Add(nameof(this.Order).FirstCharacterToLower(), this.Order);
        }

        /// <summary>
        ///     Gets the answer of a question.
        /// </summary>
        [JsonProperty("answer", Required = Required.Always, Order = 10)]
        public string Answer { get; }

        /// <summary>
        ///     Indicates if the answer is a valid answer or an info text.
        /// </summary>
        [JsonProperty("selectable", Required = Required.Always, Order = 11)]
        public bool Selectable { get; }

        /// <summary>
        ///     Gets the sorting order.
        /// </summary>
        [JsonProperty("order", Required = Required.Always, Order = 12)]
        public int Order { get; }
    }
}

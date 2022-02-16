namespace Surveys.Common.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

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
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        public Choice(string id, string answer, bool selectable)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(answer));
            }

            this.Answer = answer;
            this.Selectable = selectable;
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
    }
}

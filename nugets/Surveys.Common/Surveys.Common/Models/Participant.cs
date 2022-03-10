namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes a participant of a survey.
    /// </summary>
    public class Participant : Person, IParticipant
    {
        /// <summary>
        ///     Json name of property <see cref="Order" />.
        /// </summary>
        private const string OrderName = "order";

        /// <summary>
        ///     Json name of property <see cref="QuestionReferences" />.
        /// </summary>
        private const string QuestionReferencesName = "questionReferences";

        /// <summary>
        ///     Creates a new instance of <see cref="Person" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="email">The email of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="questionReferences">Suggested answers for questions.</param>
        /// <param name="order">The sorting order.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="email" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="name" /> is null or whitespace.</exception>
        public Participant(
            string id,
            string email,
            string name,
            IEnumerable<QuestionReference> questionReferences,
            int order
        )
            : base(id, email, name)
        {
            this.QuestionReferences = questionReferences;
            this.Order = order;
        }

        /// <summary>
        ///     Gets the suggested answers of survey questions.
        /// </summary>
        [JsonProperty(QuestionReferencesName, Required = Required.Always, Order = 100)]
        public IEnumerable<IQuestionReference> QuestionReferences { get; }

        /// <summary>
        ///     Gets the sorting order.
        /// </summary>
        [JsonProperty(OrderName, Required = Required.Always, Order = 101)]
        public int Order { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(QuestionReferencesName, this.QuestionReferences.Select(qr => qr.ToDictionary()));
            dictionary.Add(OrderName, this.Order);
            return dictionary;
        }
    }
}

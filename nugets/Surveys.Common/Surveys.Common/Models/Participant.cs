namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    public class Participant : Person, IParticipant
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Person" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="email">The email of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <param name="questionReferences">Suggested answers for questions.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="email" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="name" /> is null or whitespace.</exception>
        public Participant(
            string id,
            string email,
            string name,
            IEnumerable<QuestionReference> questionReferences
        )
            : base(id, email, name)
        {
            this.QuestionReferences = questionReferences;
        }

        /// <summary>
        ///     Add the object values to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public override void AddToDictionary(Dictionary<string, object> document)
        {
            base.AddToDictionary(document);
            document.Add(
                nameof(this.QuestionReferences).FirstCharacterToLower(),
                this.QuestionReferences.Select(qr => qr.ToDictionary()));
        }

        /// <summary>
        ///     Gets the suggested answers of survey questions.
        /// </summary>
        [JsonProperty("questionReferences", Required = Required.Always, Order = 100)]
        public IEnumerable<IQuestionReference> QuestionReferences { get; }
    }
}

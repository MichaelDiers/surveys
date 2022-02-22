namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    /// <summary>
    ///     Describes a survey organizer and is base class for participants.
    /// </summary>
    public class Person : Base, IPerson
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Person" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="email">The email of the person.</param>
        /// <param name="name">The name of the person.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="email" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="name" /> is null or whitespace.</exception>
        public Person(string id, string email, string name)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.Email = email;
            this.Name = name;
        }

        /// <summary>
        ///     Add the object values to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public override void AddToDictionary(Dictionary<string, object?> document)
        {
            base.AddToDictionary(document);
            document.Add(nameof(this.Name).FirstCharacterToLower(), this.Name);
            document.Add(nameof(this.Email).FirstCharacterToLower(), this.Email);
        }

        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        [JsonProperty("email", Required = Required.Always, Order = 10)]
        public string Email { get; }

        /// <summary>
        ///     Gets the name of the person.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 11)]
        public string Name { get; }
    }
}

namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes a survey organizer and is base class for participants.
    /// </summary>
    public class Person : Base, IPerson
    {
        /// <summary>
        ///     Json name of property <see cref="Email" />.
        /// </summary>
        private const string EmailName = "email";

        /// <summary>
        ///     Json name of property <see cref="Name" />.
        /// </summary>
        private const string NameName = "name";

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
            this.Email = email.ValidateIsAnEmail(nameof(email));
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
        }

        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        [JsonProperty(EmailName, Required = Required.Always, Order = 10)]
        public string Email { get; }

        /// <summary>
        ///     Gets the name of the person.
        /// </summary>
        [JsonProperty(NameName, Required = Required.Always, Order = 11)]
        public string Name { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(NameName, this.Name);
            dictionary.Add(EmailName, this.Email);
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="Person" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="Person" />.</returns>
        public new static Person FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = Base.FromDictionary(dictionary);
            var email = dictionary.GetString(EmailName);
            var name = dictionary.GetString(NameName);

            return new Person(baseObject.Id, email, name);
        }
    }
}

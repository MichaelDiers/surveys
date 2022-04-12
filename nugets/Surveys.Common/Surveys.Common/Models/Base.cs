namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the base for models.
    /// </summary>
    public class Base : ToDictionaryConverter, IBase
    {
        /// <summary>
        ///     Json name of the id property.
        /// </summary>
        public const string IdName = "id";

        /// <summary>
        ///     Creates a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        public Base(string id)
        {
            this.Id = id.ValidateIsAGuid(nameof(id));
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [JsonProperty(Base.IdName, Required = Required.Always, Order = 1)]
        public string Id { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(Base.IdName, this.Id);
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="Base" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="Base" />.</returns>
        public static IBase FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(Base.IdName);

            return new Base(id);
        }
    }
}

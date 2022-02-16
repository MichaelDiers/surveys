namespace Surveys.Common.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the base for models.
    /// </summary>
    public class Base : IBase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        public Base(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
            }

            if (!Guid.TryParse(id, out var guid) || guid == Guid.Empty)
            {
                throw new ArgumentException("Value is not a valid guid.", nameof(id));
            }

            this.Id = id;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [JsonProperty("id", Required = Required.Always, Order = 1)]
        public string Id { get; }
    }
}

namespace Surveys.Common.Messages
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Base definition of pub/sub messages.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="processId">The id of of the process.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="processId" /> is null or whitespace.</exception>
        public Message(string processId)
        {
            if (string.IsNullOrWhiteSpace(processId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(processId));
            }

            this.ProcessId = processId;
        }

        /// <summary>
        ///     Gets the process id.
        /// </summary>
        [JsonProperty("processId", Required = Required.AllowNull, Order = 1)]
        public string ProcessId { get; }
    }
}

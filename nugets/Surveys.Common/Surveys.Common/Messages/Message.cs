namespace Surveys.Common.Messages
{
    using System;
    using Md.Common.Extensions;
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
            this.ProcessId = processId.ValidateIsAGuid(nameof(processId));
        }

        /// <summary>
        ///     Gets the process id.
        /// </summary>
        [JsonProperty("processId", Required = Required.AllowNull, Order = 1)]
        public string ProcessId { get; }
    }
}

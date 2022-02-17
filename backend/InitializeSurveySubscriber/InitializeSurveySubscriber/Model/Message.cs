namespace InitializeSurveySubscriber.Model
{
    using System;
    using InitializeSurveySubscriber.Contracts;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Describes the incoming pub/sub message.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="survey">The survey data.</param>
        /// <param name="processId">The id of the current process.</param>
        [JsonConstructor]
        public Message(Survey survey, string processId)
            : this(survey as ISurvey, processId)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="survey">The survey data.</param>
        /// <param name="processId">The id of the current process.</param>
        public Message(ISurvey survey, string processId)
        {
            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
            this.ProcessId = processId;
            if (string.IsNullOrWhiteSpace(processId))
            {
                this.ProcessId = Guid.NewGuid().ToString();
            }
            else if (!Guid.TryParse(processId, out var id) || id == Guid.Empty)
            {
                throw new ArgumentException("Value is not a valid guid.", nameof(processId));
            }
        }

        /// <summary>
        ///     Gets the id of the current process.
        /// </summary>
        [JsonProperty("processId", Required = Required.AllowNull, Order = 11)]
        public string ProcessId { get; }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 10)]
        public ISurvey Survey { get; }
    }
}

namespace Surveys.Common.Messages
{
    using System;
    using Md.GoogleCloud.Base.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Describes a pub/sub message for initializing a survey.
    /// </summary>
    public class InitializeSurveyMessage : Message, IInitializeSurveyMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="InitializeSurveyMessage" />.
        /// </summary>
        /// <param name="survey">The survey data.</param>
        /// <param name="processId">The id of the current process.</param>
        [JsonConstructor]
        public InitializeSurveyMessage(Survey survey, string processId)
            : this(survey as ISurvey, processId)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="survey">The survey data.</param>
        /// <param name="processId">The id of the current process.</param>
        public InitializeSurveyMessage(ISurvey survey, string processId)
            : base(string.IsNullOrWhiteSpace(processId) ? Guid.NewGuid().ToString() : processId)
        {
            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
        }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 11)]
        public ISurvey Survey { get; }
    }
}

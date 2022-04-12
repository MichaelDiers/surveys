namespace Surveys.Common.Messages
{
    using System;
    using Md.Common.Extensions;
    using Md.Common.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Describes a pub/sub message for saving a survey.
    /// </summary>
    public class SaveSurveyMessage : Message, ISaveSurveyMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyMessage" />.
        /// </summary>
        /// <param name="survey">The data of the survey.</param>
        /// <param name="internalSurveyId">THe internal id of the survey.</param>
        /// <param name="processId">The id of the process.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="survey" /> is null.</exception>
        /// <exception cref="ArgumentException">It thrown if <paramref name="internalSurveyId" /> is null or whitespace.</exception>
        [JsonConstructor]
        public SaveSurveyMessage(Survey survey, string internalSurveyId, string processId)
            : this(survey as ISurvey, internalSurveyId, processId)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyMessage" />.
        /// </summary>
        /// <param name="survey">The data of the survey.</param>
        /// <param name="internalSurveyId">THe internal id of the survey.</param>
        /// <param name="processId">The id of the process.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="survey" /> is null.</exception>
        /// <exception cref="ArgumentException">It thrown if <paramref name="internalSurveyId" /> is null or whitespace.</exception>
        public SaveSurveyMessage(ISurvey survey, string internalSurveyId, string processId)
            : base(processId)
        {
            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
            this.InternalSurveyId = internalSurveyId.ValidateIsAGuid(nameof(internalSurveyId));
        }

        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        [JsonProperty("internalSurveyId", Required = Required.Always, Order = 11)]
        public string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 12)]
        public ISurvey Survey { get; }
    }
}

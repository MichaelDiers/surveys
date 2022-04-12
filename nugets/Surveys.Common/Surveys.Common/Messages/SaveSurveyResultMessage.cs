namespace Surveys.Common.Messages
{
    using System;
    using Md.Common.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Message that describes a survey result.
    /// </summary>
    public class SaveSurveyResultMessage : Message, ISaveSurveyResultMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="processId">The id of of the process.</param>
        /// <param name="surveyResult">The survey result of a participant.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="processId" /> is null or whitespace.</exception>
        [JsonConstructor]
        public SaveSurveyResultMessage(string processId, SurveyResult surveyResult)
            : this(processId, surveyResult as ISurveyResult)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="processId">The id of of the process.</param>
        /// <param name="surveyResult">The survey result of a participant.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="processId" /> is null or whitespace.</exception>
        public SaveSurveyResultMessage(string processId, ISurveyResult surveyResult)
            : base(processId)
        {
            this.SurveyResult = surveyResult ?? throw new ArgumentNullException(nameof(surveyResult));
        }

        /// <summary>
        ///     Gets the survey result.
        /// </summary>
        [JsonProperty("surveyResult", Required = Required.Always, Order = 11)]
        public ISurveyResult SurveyResult { get; }
    }
}

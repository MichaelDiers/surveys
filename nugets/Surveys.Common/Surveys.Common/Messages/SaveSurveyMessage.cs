namespace Surveys.Common.Messages
{
    using System;
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
        /// <param name="processId">The id of the process.</param>
        /// <param name="survey">The data of the survey.</param>
        [JsonConstructor]
        public SaveSurveyMessage(string processId, Survey survey)
            : this(processId, survey as ISurvey)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyMessage" />.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <param name="survey">The data of the survey.</param>
        public SaveSurveyMessage(string processId, ISurvey survey)
            : base(processId)
        {
            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
        }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 12)]
        public ISurvey Survey { get; }
    }
}

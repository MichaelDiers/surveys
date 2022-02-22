namespace Surveys.Common.Messages
{
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Save a new status of a survey.
    /// </summary>
    public class SaveSurveyStatusMessage : Message, ISaveSurveyStatusMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="surveyStatus">The status data.</param>
        [JsonConstructor]
        public SaveSurveyStatusMessage(string processId, SurveyStatus surveyStatus)
            : this(processId, surveyStatus as ISurveyStatus)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="surveyStatus">The status data.</param>
        public SaveSurveyStatusMessage(string processId, ISurveyStatus surveyStatus)
            : base(processId)
        {
            this.SurveyStatus = surveyStatus;
        }

        /// <summary>
        ///     Gets the survey status data.
        /// </summary>
        [JsonProperty("surveyStatus", Required = Required.Always, Order = 11)]
        public ISurveyStatus SurveyStatus { get; }
    }
}

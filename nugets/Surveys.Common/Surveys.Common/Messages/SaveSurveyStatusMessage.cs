namespace Surveys.Common.Messages
{
    using Md.Common.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
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
        /// <param name="surveyClosedMessage">A survey closed message that is sent if the status is created.</param>
        [JsonConstructor]
        public SaveSurveyStatusMessage(
            string processId,
            SurveyStatus surveyStatus,
            SurveyClosedMessage surveyClosedMessage
        )
            : this(processId, surveyStatus, surveyClosedMessage as ISurveyClosedMessage)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveSurveyStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="surveyStatus">The status data.</param>
        /// <param name="surveyClosedMessage">A survey closed message that is sent if the status is created.</param>
        public SaveSurveyStatusMessage(
            string processId,
            ISurveyStatus surveyStatus,
            ISurveyClosedMessage surveyClosedMessage
        )
            : base(processId)
        {
            this.SurveyStatus = surveyStatus;
            this.SurveyClosedMessage = surveyClosedMessage;
        }

        /// <summary>
        ///     Gets the survey status data.
        /// </summary>
        [JsonProperty("surveyClosedMessage", Required = Required.Always, Order = 12)]
        public ISurveyClosedMessage SurveyClosedMessage { get; }

        /// <summary>
        ///     Gets the survey status data.
        /// </summary>
        [JsonProperty("surveyStatus", Required = Required.Always, Order = 11)]
        public ISurveyStatus SurveyStatus { get; }
    }
}

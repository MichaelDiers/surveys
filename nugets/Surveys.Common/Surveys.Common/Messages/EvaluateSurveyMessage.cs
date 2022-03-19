namespace Surveys.Common.Messages
{
    using Md.GoogleCloud.Base.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Describes the valuate survey message.
    /// </summary>
    public class EvaluateSurveyMessage : Message, IEvaluateSurveyMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="EvaluateSurveyMessage" />
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="internalSurveyId">The internal survey id.</param>
        public EvaluateSurveyMessage(string processId, string internalSurveyId)
            : base(processId)
        {
            this.InternalSurveyId = internalSurveyId;
        }

        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        [JsonProperty("internalSurveyId", Required = Required.Always, Order = 11)]
        public string InternalSurveyId { get; }
    }
}

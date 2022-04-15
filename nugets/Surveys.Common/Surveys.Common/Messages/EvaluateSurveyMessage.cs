namespace Surveys.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Common.Messages;
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
        /// <param name="surveyDocumentId">The id of the survey.</param>
        public EvaluateSurveyMessage(string processId, string surveyDocumentId)
            : base(processId)
        {
            this.SurveyDocumentId = surveyDocumentId.ValidateIsAGuid(nameof(surveyDocumentId));
        }

        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        [JsonProperty("surveyDocumentId", Required = Required.Always, Order = 11)]
        public string SurveyDocumentId { get; }
    }
}

namespace Surveys.Common.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.GoogleCloud.Base.Messages;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Models;

    /// <summary>
    ///     Describes a closed survey message.
    /// </summary>
    public class SurveyClosedMessage : Message, ISurveyClosedMessage
    {
        /// <summary>
        ///     Creates a new <see cref="SurveyClosedMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="survey">The survey data.</param>
        /// <param name="results">The survey results.</param>
        [JsonConstructor]
        public SurveyClosedMessage(string processId, Survey survey, IEnumerable<SurveyResult> results)
            : this(processId, survey, results.Select(x => x as ISurveyResult).ToArray())
        {
        }

        /// <summary>
        ///     Creates a new <see cref="SurveyClosedMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="survey">The survey data.</param>
        /// <param name="results">The survey results.</param>
        public SurveyClosedMessage(string processId, ISurvey survey, IEnumerable<ISurveyResult> results)
            : base(processId)
        {
            this.Survey = survey;
            this.Results = results;
        }

        /// <summary>
        ///     Gets the survey results.
        /// </summary>
        [JsonProperty("results", Required = Required.Always, Order = 12)]
        public IEnumerable<ISurveyResult> Results { get; }

        /// <summary>
        ///     Gets the survey.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 11)]
        public ISurvey Survey { get; }
    }
}

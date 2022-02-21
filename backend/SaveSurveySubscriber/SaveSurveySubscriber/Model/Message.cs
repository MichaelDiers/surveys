namespace SaveSurveySubscriber.Model
{
    using System;
    using Newtonsoft.Json;
    using SaveSurveySubscriber.Contracts;
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
        /// <param name="survey">The data of the survey.</param>
        /// <param name="internalSurveyId">THe internal id of the survey.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="survey" /> is null.</exception>
        /// <exception cref="ArgumentException">It thrown if <paramref name="internalSurveyId" /> is null or whitespace.</exception>
        public Message(Survey survey, string internalSurveyId)
        {
            if (string.IsNullOrWhiteSpace(internalSurveyId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(internalSurveyId));
            }

            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
            this.InternalSurveyId = internalSurveyId;
        }

        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        [JsonProperty("internalSurveyId", Required = Required.Always, Order = 1)]
        public string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        [JsonProperty("survey", Required = Required.Always, Order = 2)]
        public ISurvey Survey { get; }
    }
}

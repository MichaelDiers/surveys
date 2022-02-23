namespace Surveys.Common.Messages
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Models;

    /// <summary>
    ///     The data specifies for <see cref="MailType.RequestForParticipation" />.
    /// </summary>
    public class RequestForParticipation : IRequestForParticipation
    {
        /// <summary>
        ///     Creates a new instance of <see cref="RequestForParticipation" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal survey id.</param>
        /// <param name="survey">The survey data.</param>
        [JsonConstructor]
        public RequestForParticipation(string internalSurveyId, Survey survey)
            : this(internalSurveyId, survey as ISurvey)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="RequestForParticipation" />.
        /// </summary>
        /// <param name="internalSurveyId">The internal survey id.</param>
        /// <param name="survey">The survey data.</param>
        public RequestForParticipation(string internalSurveyId, ISurvey survey)
        {
            if (string.IsNullOrWhiteSpace(internalSurveyId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(internalSurveyId));
            }

            this.InternalSurveyId = internalSurveyId;
            this.Survey = survey ?? throw new ArgumentNullException(nameof(survey));
        }

        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        public string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        public ISurvey Survey { get; }
    }
}

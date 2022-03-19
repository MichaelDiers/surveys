﻿namespace Surveys.Common.Messages
{
    using Md.GoogleCloud.Base.Messages;

    /// <summary>
    ///     Describes the valuate survey message.
    /// </summary>
    public class EvaluateSurveyMessage : Message
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
        public string InternalSurveyId { get; }
    }
}

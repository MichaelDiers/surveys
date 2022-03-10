namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Describes a survey result.
    /// </summary>
    public interface ISurveyResult : IToDictionary
    {
        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        string InternalSurveyId { get; }

        /// <summary>
        ///     Gets a value that indicates if the result is a suggested result or a real survey result.
        /// </summary>
        bool IsSuggested { get; }

        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        string ParticipantId { get; }

        /// <summary>
        ///     Gets the survey results.
        /// </summary>
        IEnumerable<IQuestionReference> Results { get; }
    }
}

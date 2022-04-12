namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a survey result.
    /// </summary>
    public interface ISurveyResult : IDatabaseObject
    {
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

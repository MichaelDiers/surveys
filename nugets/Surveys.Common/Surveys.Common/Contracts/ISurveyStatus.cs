namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public interface ISurveyStatus : IDatabaseObject
    {
        /// <summary>
        ///     Gets the id of the participant.
        /// </summary>
        string ParticipantId { get; }

        /// <summary>
        ///     Gets the status of the survey.
        /// </summary>
        Status Status { get; }
    }
}

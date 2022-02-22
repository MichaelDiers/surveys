namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public interface ISurveyStatus : IDictionaryConverter
    {
        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        string InternalSurveyId { get; }

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

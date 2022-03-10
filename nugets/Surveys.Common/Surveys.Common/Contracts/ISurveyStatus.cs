namespace Surveys.Common.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    public interface ISurveyStatus : IToDictionary
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

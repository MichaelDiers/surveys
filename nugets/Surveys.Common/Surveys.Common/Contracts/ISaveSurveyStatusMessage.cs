namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Save a new status of a survey.
    /// </summary>
    public interface ISaveSurveyStatusMessage : IMessage
    {
        /// <summary>
        ///     Gets the survey status data.
        /// </summary>
        ISurveyStatus SurveyStatus { get; }
    }
}

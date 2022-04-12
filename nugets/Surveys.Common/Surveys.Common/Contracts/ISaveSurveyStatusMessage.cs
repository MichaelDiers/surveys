namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Messages;

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

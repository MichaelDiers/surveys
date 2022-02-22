namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Message that describes a survey result.
    /// </summary>
    public interface ISaveSurveyResultMessage : IMessage
    {
        /// <summary>
        ///     Gets the survey result.
        /// </summary>
        ISurveyResult SurveyResult { get; }
    }
}

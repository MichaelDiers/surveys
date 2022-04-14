namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Describes a pub/sub message for saving a survey.
    /// </summary>
    public interface ISaveSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        ISurvey Survey { get; }
    }
}

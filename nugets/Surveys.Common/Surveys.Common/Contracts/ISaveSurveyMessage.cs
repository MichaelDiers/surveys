namespace Surveys.Common.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Messages;

    /// <summary>
    ///     Describes a pub/sub message for saving a survey.
    /// </summary>
    public interface ISaveSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        ISurvey Survey { get; }
    }
}

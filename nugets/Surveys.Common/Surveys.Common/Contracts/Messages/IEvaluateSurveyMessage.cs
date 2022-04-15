namespace Surveys.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Describes the valuate survey message.
    /// </summary>
    public interface IEvaluateSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        string SurveyDocumentId { get; }
    }
}

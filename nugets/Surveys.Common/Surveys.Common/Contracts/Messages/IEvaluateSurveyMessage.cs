namespace Surveys.Common.Contracts.Messages
{
    using Md.GoogleCloud.Base.Contracts.Messages;

    /// <summary>
    ///     Describes the valuate survey message.
    /// </summary>
    public interface IEvaluateSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the internal id of the survey.
        /// </summary>
        public string InternalSurveyId { get; }
    }
}

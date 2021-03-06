namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Describes a pub/sub message for initializing a survey.
    /// </summary>
    public interface IInitializeSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        ISurvey Survey { get; }
    }
}

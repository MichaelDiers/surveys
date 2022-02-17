namespace InitializeSurveySubscriber.Contracts
{
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the incoming pub/sub message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///     Gets the id of the current process.
        /// </summary>
        string ProcessId { get; }

        /// <summary>
        ///     Gets the survey data.
        /// </summary>
        ISurvey Survey { get; }
    }
}

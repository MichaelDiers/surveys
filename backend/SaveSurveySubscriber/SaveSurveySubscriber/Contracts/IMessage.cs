namespace SaveSurveySubscriber.Contracts
{
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the incoming pub/sub message.
    /// </summary>
    public interface IMessage
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

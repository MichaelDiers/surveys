namespace Surveys.Common.Contracts.Messages
{
    /// <summary>
    ///     The data specifies for <see cref="MailType.RequestForParticipation" />.
    /// </summary>
    public interface IRequestForParticipation
    {
        /// <summary>
        ///     Gets the internal survey id.
        /// </summary>
        string InternalSurveyId { get; }

        /// <summary>
        ///     Gets the data of the survey.
        /// </summary>
        ISurvey Survey { get; }
    }
}

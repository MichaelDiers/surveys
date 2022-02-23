namespace Surveys.Common.Contracts.Messages
{
    /// <summary>
    ///     Specifies the type of an email.
    /// </summary>
    public enum MailType
    {
        /// <summary>
        ///     Undefined type.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Request for participating in a survey.
        /// </summary>
        RequestForParticipation = 1
    }
}

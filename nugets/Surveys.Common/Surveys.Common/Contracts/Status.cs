namespace Surveys.Common.Contracts
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    ///     Describes the status of a survey.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        /// <summary>
        ///     Undefined status.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Survey is created.
        /// </summary>
        [EnumMember(Value = "CREATED")]
        Created,

        /// <summary>
        ///     A survey invitation is sent.
        /// </summary>
        [EnumMember(Value = "INVITATION_MAIL_OK")]
        InvitationMailSentOk,

        /// <summary>
        ///     Sending a survey invitation mail failed.
        /// </summary>
        [EnumMember(Value = "INVITATION_MAIL_FAILED")]
        InvitationMailSentFailed
    }
}

namespace Surveys.Common.Contracts
{
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
        Created
    }
}

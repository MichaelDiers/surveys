namespace Surveys.Common.Tests.Contracts
{
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    internal class StatusTestsHelper
    {
        [JsonConstructor]
        public StatusTestsHelper(Status status)
        {
            this.Status = status;
        }

        [JsonProperty("status", Required = Required.Always, Order = 1)]
        public Status Status { get; }
    }
}

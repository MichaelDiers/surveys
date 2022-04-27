namespace Surveys.Common.Messages
{
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    public class Attachment : IAttachment
    {
        public Attachment(string name, byte[] data)
        {
            this.Name = name;
            this.Data = data;
        }

        [JsonProperty("data", Required = Required.Always, Order = 2)]
        public byte[] Data { get; }

        [JsonProperty("name", Required = Required.Always, Order = 1)]
        public string Name { get; }
    }
}

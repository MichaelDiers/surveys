namespace CreateMailSubscriber.Tests.Data
{
    using System.IO;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    internal static class TestData
    {
        public static ICreateMailMessage CreateMailMessage()
        {
            return JsonConvert.DeserializeObject<CreateMailMessage>(File.ReadAllText("Data/message.json"));
        }
    }
}

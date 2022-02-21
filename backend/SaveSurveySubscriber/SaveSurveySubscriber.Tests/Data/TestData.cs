namespace SaveSurveySubscriber.Tests.Data
{
    using System.IO;
    using Newtonsoft.Json;
    using SaveSurveySubscriber.Contracts;
    using SaveSurveySubscriber.Model;

    internal static class TestData
    {
        public static IMessage InitializeMessage()
        {
            const string file = "Data/Message.json";
            return JsonConvert.DeserializeObject<Message>(File.ReadAllText(file));
        }

        public static IMessage InitializeMessage(string internalSurveyId)
        {
            var message = InitializeMessage();
            return new Message(message.Survey, internalSurveyId);
        }
    }
}

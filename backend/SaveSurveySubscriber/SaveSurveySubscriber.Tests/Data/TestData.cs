namespace SaveSurveySubscriber.Tests.Data
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;

    internal static class TestData
    {
        public static ISaveSurveyMessage InitializeMessage()
        {
            const string file = "Data/Message.json";
            return JsonConvert.DeserializeObject<SaveSurveyMessage>(File.ReadAllText(file));
        }

        public static ISaveSurveyMessage InitializeMessage(string internalSurveyId)
        {
            var message = InitializeMessage();
            return new SaveSurveyMessage(message.Survey, internalSurveyId, Guid.NewGuid().ToString());
        }
    }
}

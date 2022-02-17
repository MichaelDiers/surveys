namespace InitializeSurveySubscriber.Tests.Data
{
    using System.IO;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    internal static class TestData
    {
        public static ISurvey InitializeSurvey()
        {
            var json = new FileInfo("Data/Survey.json").OpenText().ReadToEnd();
            return JsonConvert.DeserializeObject<Survey>(json);
        }
    }
}

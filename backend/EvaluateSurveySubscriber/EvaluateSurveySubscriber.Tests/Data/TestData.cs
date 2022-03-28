namespace EvaluateSurveySubscriber.Tests.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;

    internal static class TestData
    {
        public static IEvaluateSurveyMessage CreateMessage()
        {
            return JsonConvert.DeserializeObject<EvaluateSurveyMessage>(File.ReadAllText("Data/message.json"));
        }

        public static IEnumerable<ISurveyResult> CreateResults(string internalSurveyId, bool allVoted)
        {
            var survey = TestData.CreateSurvey();
            var results = survey.Participants.Select(
                    p => new SurveyResult(
                        internalSurveyId,
                        p.Id,
                        true,
                        survey.Questions.Select(q => new QuestionReference(q.Id, q.Choices.First().Id)).ToArray()))
                .ToList();

            var participants = TestData.CreateSurvey().Participants;
            if (!allVoted)
            {
                participants = participants.Skip(1);
            }

            results.AddRange(
                participants.Select(
                    (p, i) => new SurveyResult(
                        internalSurveyId,
                        p.Id,
                        false,
                        survey.Questions.Select(q => new QuestionReference(q.Id, q.Choices.Skip(i).First().Id))
                            .ToArray())));
            return results;
        }

        public static IEnumerable<ISurveyStatus> CreateStatus(string internalSurveyId, bool isClosed)
        {
            var survey = TestData.CreateSurvey();
            var status = Enumerable.Range(0, 10)
                .Select(_ => new SurveyStatus(internalSurveyId, Status.Created))
                .ToList();
            if (isClosed)
            {
                status.Add(new SurveyStatus(internalSurveyId, Status.Closed));
            }

            return status;
        }

        public static ISurvey CreateSurvey()
        {
            return JsonConvert.DeserializeObject<Survey>(File.ReadAllText("Data/survey.json"));
        }
    }
}

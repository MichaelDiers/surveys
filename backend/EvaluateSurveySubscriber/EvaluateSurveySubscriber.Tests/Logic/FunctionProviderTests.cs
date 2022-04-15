namespace EvaluateSurveySubscriber.Tests.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Logic;
    using Google.Cloud.Functions.Testing;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="FunctionProvider" />
    /// </summary>
    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var container = new TestDataContainer();
            Assert.Empty(
                await FunctionProviderTests.Run(
                    container.Game,
                    container.Survey,
                    container.SurveyStatus,
                    container.SurveyResults));
        }

        [Fact]
        public async void HandleAsyncCheckSurveyClosedMessage()
        {
            var container = new TestDataContainer();
            var results = new List<ISurveyResult>();
            var surveyClosedMessage = new SurveyClosedMessage(
                Guid.NewGuid().ToString(),
                container.Survey as Survey,
                container.GameSeries.Players.Select(
                        (player, i) =>
                        {
                            var surveyResult = new SurveyResult(
                                Guid.NewGuid().ToString(),
                                DateTime.Now,
                                container.Survey.DocumentId,
                                player.Id,
                                false,
                                container.Survey.Questions.Select(
                                        question => new QuestionReference(
                                            question.Id,
                                            question.Choices.Where(choice => choice.Selectable).Skip(i).First().Id))
                                    .ToArray());
                            results.Add(surveyResult);
                            return surveyResult;
                        })
                    .ToArray());

            Assert.Empty(
                await FunctionProviderTests.Run(
                    container.Game,
                    container.Survey,
                    Enumerable.Empty<ISurveyStatus>(),
                    results,
                    surveyClosedMessage));
        }

        [Fact]
        public async void HandleAsyncFailsForClosedSurvey()
        {
            var container = new TestDataContainer();
            var gameSeries = container.GameSeries;
            var game = container.Game;
            var survey = container.Survey;
            var status = new[]
            {
                new SurveyStatus(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    survey.DocumentId,
                    Status.Closed)
            };
            var results = Enumerable.Empty<ISurveyResult>();

            var logs = await FunctionProviderTests.Run(
                game,
                survey,
                status,
                results);
            Assert.Single(logs);
            Assert.Equal($"Survey {survey.DocumentId} is already closed.", logs.First().Exception?.Message);
        }

        private static async Task<List<TestLogEntry>> Run(
            IGame game,
            ISurvey survey,
            IEnumerable<ISurveyStatus> status,
            IEnumerable<ISurveyResult> results
        )
        {
            return await FunctionProviderTests.Run(
                game,
                survey,
                status,
                results,
                null);
        }

        private static async Task<List<TestLogEntry>> Run(
            IGame game,
            ISurvey survey,
            IEnumerable<ISurveyStatus> status,
            IEnumerable<ISurveyResult> results,
            ISurveyClosedMessage? expectedSurveyClosedMessage
        )
        {
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveysDatabaseMock(new Dictionary<string, ISurvey> {{survey.DocumentId, survey}}),
                new SurveyResultsDatabaseMock(results),
                new SurveyStatusDatabaseMock(status),
                new SaveSurveyStatusPubSubClientMock(),
                new SurveyClosedPubSubClientMock(expectedSurveyClosedMessage));

            await provider.HandleAsync(
                new EvaluateSurveyMessage(
                    expectedSurveyClosedMessage?.ProcessId ?? Guid.NewGuid().ToString(),
                    survey.DocumentId));
            return logger.ListLogEntries();
        }
    }
}

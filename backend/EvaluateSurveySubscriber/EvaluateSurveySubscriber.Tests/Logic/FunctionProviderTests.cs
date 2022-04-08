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
            var gameSeries = GameSeriesGenerator.Generate();
            var game = GameGenerator.Generate(new GameGeneratorConfiguration(), gameSeries);
            var survey = SurveyGenerator.Generate(new SurveyGeneratorConfiguration(), gameSeries, game);
            var status = SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration(), game).ToArray();
            var results = SurveyResultGenerator.Generate(new SurveyResultGeneratorConfiguration(), game, survey)
                .ToArray();
            Assert.Empty(
                await FunctionProviderTests.Run(
                    game,
                    survey,
                    status,
                    results));
        }

        [Fact]
        public async void HandleAsyncCheckSurveyClosedMessage()
        {
            var gameSeries = GameSeriesGenerator.Generate();
            var game = GameGenerator.Generate(new GameGeneratorConfiguration(), gameSeries);
            var survey = SurveyGenerator.Generate(new SurveyGeneratorConfiguration(), gameSeries, game);
            var status = SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration(), game).ToArray();
            var results = SurveyResultGenerator.Generate(new SurveyResultGeneratorConfiguration(), game, survey)
                .ToList();


            var surveyClosedMessage = new SurveyClosedMessage(
                Guid.NewGuid().ToString(),
                survey,
                gameSeries.Players.Select(
                    (player, i) =>
                    {
                        var surveyResult = new SurveyResult(
                            game.SurveyId,
                            player.Id,
                            false,
                            survey.Questions.Select(
                                    question => new QuestionReference(
                                        question.Id,
                                        question.Choices.Where(choice => choice.Selectable).Skip(i).First().Id))
                                .ToArray());
                        results.Add(surveyResult);
                        return surveyResult;
                    }));

            Assert.Empty(
                await FunctionProviderTests.Run(
                    game,
                    survey,
                    status,
                    results,
                    surveyClosedMessage));
        }

        [Fact]
        public async void HandleAsyncFailsForClosedSurvey()
        {
            var gameSeries = GameSeriesGenerator.Generate();
            var game = GameGenerator.Generate(new GameGeneratorConfiguration(), gameSeries);
            var survey = SurveyGenerator.Generate(new SurveyGeneratorConfiguration(), gameSeries, game);
            var status = SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration {IsClosed = true}, game)
                .ToArray();
            var results = SurveyResultGenerator.Generate(new SurveyResultGeneratorConfiguration(), game, survey)
                .ToArray();

            var logs = await FunctionProviderTests.Run(
                game,
                survey,
                status,
                results);
            Assert.Single(logs);
            Assert.Equal($"Survey {game.SurveyId} is already closed.", logs.First().Exception?.Message);
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
                new SurveysDatabaseMock(new Dictionary<string, ISurvey> {{game.SurveyId, survey}}),
                new SurveyResultsDatabaseMock(results),
                new SurveyStatusDatabaseMock(status),
                new SaveSurveyStatusPubSubClientMock(),
                new SurveyClosedPubSubClientMock(expectedSurveyClosedMessage));

            await provider.HandleAsync(
                new EvaluateSurveyMessage(
                    expectedSurveyClosedMessage?.ProcessId ?? Guid.NewGuid().ToString(),
                    game.SurveyId));
            return logger.ListLogEntries();
        }
    }
}

namespace EvaluateSurveySubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using EvaluateSurveySubscriber.Logic;
    using EvaluateSurveySubscriber.Model;
    using EvaluateSurveySubscriber.Tests.Data;
    using EvaluateSurveySubscriber.Tests.Mocks;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var message = TestData.CreateMessage();
            await HandleAsyncForMessage(message);
        }

        [Fact]
        public async void HandleAsyncWithProvider()
        {
            var message = TestData.CreateMessage();
            await HandleAsyncForMessageWithProvider(message);
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabase()
        {
            var message = new EvaluateSurveyMessage(Guid.NewGuid().ToString(), "9a8f40a6-75b1-4351-90cc-7289db972760");

            await HandleAsyncIntegration(message);
        }


        private static async Task HandleAsyncForMessage(IEvaluateSurveyMessage message)
        {
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = data
            };

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProviderMock(message);
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }

        private static async Task HandleAsyncForMessageWithProvider(IEvaluateSurveyMessage message)
        {
            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            Assert.NotNull(configuration);
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = data
            };

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveyDatabaseMock(),
                new SurveyResultsDatabaseMock(true),
                new SurveyStatusDatabaseMock(false),
                new PubSubClientMock(),
                new PubSubClientMock());
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }

        private static async Task HandleAsyncIntegration(IEvaluateSurveyMessage message)
        {
            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            Assert.NotNull(configuration);
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = data
            };

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveysCollectionName)),
                new SurveyResultsDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveyResultsCollectionName)),
                new SurveyStatusDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveyStatusCollectionName)),
                new SaveSurveyStatusPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.SaveSurveyStatusTopicName)),
                new SurveyClosedPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.SurveyClosedTopicName)));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

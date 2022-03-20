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
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;
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
        /*
        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabase()
        {
            var message = TestData.CreateMessage();
            await HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClientMock(),
                config => new ReadonlyDatabase(config));
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabaseAndPubSubClient()
        {
            var message = TestData.CreateMessage();
            await HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClient(config),
                config => new ReadonlyDatabase(config));
        }*/

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
    }
}

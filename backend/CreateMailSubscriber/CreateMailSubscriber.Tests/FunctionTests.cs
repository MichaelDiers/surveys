namespace CreateMailSubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using CreateMailSubscriber.Logic;
    using CreateMailSubscriber.Model;
    using CreateMailSubscriber.Tests.Data;
    using CreateMailSubscriber.Tests.Mocks;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.GoogleCloudPubSub.Logic;
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
            var message = TestData.CreateMailMessage();
            await HandleAsyncForMessage(message);
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProvider()
        {
            var message = TestData.CreateMailMessage();
            await HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClientMock(),
                config => new DatabaseMock());
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabase()
        {
            var message = TestData.CreateMailMessage();
            await HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClientMock(),
                config => new ReadonlyDatabase(config));
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabaseAndPubSubClient()
        {
            var message = TestData.CreateMailMessage();
            await HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClient(config),
                config => new ReadonlyDatabase(config));
        }

        private static async Task HandleAsyncForMessage(ICreateMailMessage message)
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

        private static async Task HandleAsyncForMessageWithProvider(
            ICreateMailMessage message,
            Func<IPubSubClientConfiguration, IPubSubClient> pubSubClientFactory,
            Func<DatabaseConfiguration, IReadOnlyDatabase> databaseFactory
        )

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
                pubSubClientFactory(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.SendMailTopicName)),
                databaseFactory(new DatabaseConfiguration(configuration.ProjectId, configuration.CollectionName)));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

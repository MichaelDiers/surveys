namespace CreateMailSubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using CreateMailSubscriber.Model;
    using CreateMailSubscriber.Tests.Data;
    using CreateMailSubscriber.Tests.Mocks;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Contracts;
    using Md.GoogleCloudPubSub.Logic;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Firestore.Models;
    using Surveys.Common.PubSub.Contracts.Logic;
    using Surveys.Common.PubSub.Logic;
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
            await FunctionTests.HandleAsyncForMessage(message);
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProvider()
        {
            var message = TestData.CreateMailMessage();
            await FunctionTests.HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClientMock(),
                config => new DatabaseMock());
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabase()
        {
            var message = TestData.CreateMailMessage();
            await FunctionTests.HandleAsyncForMessageWithProvider(
                message,
                config => new PubSubClientMock(),
                config => new EmailTemplateReadOnlyDatabase(config));
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncWithProviderAndDatabaseAndPubSubClient()
        {
            var message = TestData.CreateMailMessage();
            await FunctionTests.HandleAsyncForMessageWithProvider(
                message,
                config => new SendMailPubSubClient(config),
                config => new EmailTemplateReadOnlyDatabase(config));
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
            Func<IPubSubClientEnvironment, ISendMailPubSubClient> pubSubClientFactory,
            Func<IRuntimeEnvironment, IEmailTemplateReadOnlyDatabase> databaseFactory
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
            /**
            var provider = new FunctionProvider(
                logger,
                pubSubClientFactory(
                    new PubSubClientEnvironment(Environment.Test, configuration.ProjectId, configuration.TopicName)),
                databaseFactory(configuration),
                configuration);
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
            */
        }
    }
}

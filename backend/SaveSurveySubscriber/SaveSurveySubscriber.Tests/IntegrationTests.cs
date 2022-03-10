namespace SaveSurveySubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Newtonsoft.Json;
    using SaveSurveySubscriber.Logic;
    using SaveSurveySubscriber.Model;
    using SaveSurveySubscriber.Tests.Data;
    using Surveys.Common.Contracts;
    using Xunit;

    /// <summary>
    ///     Integration tests for <see cref="Function" />.
    /// </summary>
    public class IntegrationTests
    {
        [Fact(Skip = "Integration only")]
        public async void HandleAsync()
        {
            var message = TestData.InitializeMessage(Guid.NewGuid().ToString());
            await HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(ISaveSurveyMessage message)
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

            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            Assert.NotNull(configuration);

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new Database(new DatabaseConfiguration(configuration.ProjectId, configuration.CollectionName)));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

namespace SaveSurveySubscriber.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
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
            var container = new TestDataContainer();
            var message = new SaveSurveyMessage(Guid.NewGuid().ToString(), container.Survey);
            await FunctionTests.HandleAsyncForMessage(message);
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

            var logger = new MemoryLogger<Function>();
            var container = new TestDataContainer();
            var provider = new FunctionProvider(
                logger,
                container.SurveysDatabaseMock,
                new CreateMailPubSubClientMock(),
                new SaveSurveyResultPubSubClientMock());
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

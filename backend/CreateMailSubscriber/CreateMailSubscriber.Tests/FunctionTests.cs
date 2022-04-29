namespace CreateMailSubscriber.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using CreateMailSubscriber.Logic;
    using CreateMailSubscriber.Model;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Logic;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void HandleAsyncRequestForParticipation()
        {
            var container = new TestDataContainer();
            var message = new CreateMailMessage(
                Guid.NewGuid().ToString(),
                MailType.RequestForParticipation,
                container.Survey);
            await FunctionTests.HandleAsyncForMessage(message);
        }

        [Fact]
        public async void HandleAsyncThankYou()
        {
            var container = new TestDataContainer();
            var message = new CreateMailMessage(
                Guid.NewGuid().ToString(),
                MailType.ThankYou,
                container.Survey,
                container.SurveyResult,
                Enumerable.Empty<string>());
            await FunctionTests.HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(ICreateMailMessage message)
        {
            var json = Serializer.SerializeObject(message);
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
                new SendMailPubSubClientMock(),
                new FunctionConfiguration {FrondEndUrlFormat = "{0}/{1}"});
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

namespace SaveSurveySubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Logic;
    using Md.Common.Model;
    using Md.Tga.Common.TestData.Generators;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Models;
    using Surveys.Common.Messages;
    using Xunit;
    using Environment = Md.Common.Contracts.Model.Environment;

    /// <summary>
    ///     Integration tests for <see cref="Function" />.
    /// </summary>
    public class IntegrationTests
    {
        [Fact(Skip = "Integration only")]
        public async void HandleAsync()
        {
            var container = new TestDataContainer();
            var survey = container.Survey;
            var message = new SaveSurveyMessage(Guid.NewGuid().ToString(), survey);
            await IntegrationTests.HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(ISaveSurveyMessage message)
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

            var configuration =
                // ReSharper disable once StringLiteralTypo
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            Assert.NotNull(configuration);

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveyDatabase(
                    new RuntimeEnvironment
                    {
                        ProjectId = configuration?.ProjectId ?? throw new NotImplementedException(),
                        Environment = Environment.Test
                    }));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

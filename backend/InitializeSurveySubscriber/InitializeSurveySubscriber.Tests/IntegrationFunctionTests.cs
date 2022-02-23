namespace InitializeSurveySubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using InitializeSurveySubscriber.Logic;
    using InitializeSurveySubscriber.Model;
    using InitializeSurveySubscriber.Tests.Data;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Logic;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class IntegrationFunctionTests
    {
        [Fact(Skip = "Integration only")]
        public async void HandleAsync()
        {
            var survey = TestData.InitializeSurvey();
            var message = new InitializeSurveyMessage(survey, Guid.NewGuid().ToString());
            await HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(IInitializeSurveyMessage message)
        {
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData
            {
                Message = new PubsubMessage
                {
                    TextData = json
                }
            };

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
                configuration,
                new PubSub(new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyTopicName)),
                new PubSub(new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyResultTopicName)),
                new PubSub(new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyStatusTopicName)),
                new PubSub(new PubSubConfiguration(configuration.ProjectId, configuration.CreateMailTopicName)));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

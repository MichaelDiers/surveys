namespace SaveSurveySubscriber.Tests
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Surveys.Common.Messages;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="FunctionProvider" />
    /// </summary>
    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveysDatabaseMock(),
                new CreateMailPubSubClientMock(),
                new SaveSurveyResultPubSubClientMock());

            var container = new TestDataContainer();
            var message = new SaveSurveyMessage(Guid.NewGuid().ToString(), container.Survey);

            await provider.HandleAsync(message);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

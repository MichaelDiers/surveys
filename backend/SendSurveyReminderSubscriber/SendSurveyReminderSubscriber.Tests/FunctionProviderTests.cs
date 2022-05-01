namespace SendSurveyReminderSubscriber.Tests
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.Common.Messages;
    using Md.Common.Model;
    using Surveys.Common.Firestore.Models;
    using Xunit;
    using Environment = Md.Common.Contracts.Model.Environment;

    public class FunctionProviderTests
    {
        [Theory(Skip = "Integration")]
        [InlineData(Environment.Test, "surveys-services-test")]
        public async void HandleAsync(Environment environment, string projectId)
        {
            var runtime = new RuntimeEnvironment {Environment = environment, ProjectId = projectId};
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SurveyReadOnlyDatabase(runtime),
                new SurveyStatusReadOnlyDatabase(runtime),
                new SurveyResultReadOnlyDatabase(runtime),
                new PubSubMock());
            await provider.HandleAsync(new Message(Guid.NewGuid().ToString()));
        }
    }
}

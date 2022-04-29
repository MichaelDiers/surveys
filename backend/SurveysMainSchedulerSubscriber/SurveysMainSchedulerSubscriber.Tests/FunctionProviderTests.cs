namespace SurveysMainSchedulerSubscriber.Tests
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.Common.Messages;
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
            var provider = new FunctionProvider(logger);

            await provider.HandleAsync(new Message(Guid.NewGuid().ToString()));

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

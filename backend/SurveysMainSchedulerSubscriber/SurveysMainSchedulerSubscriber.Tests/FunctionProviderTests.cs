namespace SurveysMainSchedulerSubscriber.Tests
{
    using System;
    using System.Linq;
    using Google.Cloud.Functions.Testing;
    using Md.Common.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;
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
            var provider = new FunctionProvider(logger, Enumerable.Empty<ISchedulerPubSubClient>());

            await provider.HandleAsync(new Message(Guid.NewGuid().ToString()));

            Assert.Empty(logger.ListLogEntries());
        }
    }
}

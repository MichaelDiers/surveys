namespace SaveSurveySubscriber.Tests.Logic
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.Functions.Testing;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using SaveSurveySubscriber.Logic;
    using SaveSurveySubscriber.Tests.Mocks;
    using Surveys.Common.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="FunctionProvider" />
    /// </summary>
    public class FunctionProviderTests
    {
        /// <summary>
        ///     Throw <see cref="ArgumentNullException" /> if the input is null.
        /// </summary>
        [Fact]
        public async void HandleAsync_Null_ArgumentNullException()
        {
            var provider = await InitAsync();
            await Assert.ThrowsAsync<ArgumentNullException>(() => provider.HandleAsync(null));
        }

        /// <summary>
        ///     Initialize the provider and its dependencies.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is an <see cref="IPubSubProvider{TMessage}" />.</returns>
        private static Task<IPubSubProvider<ISaveSurveyMessage>> InitAsync()
        {
            var provider = new FunctionProvider(new MemoryLogger<Function>(), new DatabaseMock());
            return Task.FromResult<IPubSubProvider<ISaveSurveyMessage>>(provider);
        }
    }
}

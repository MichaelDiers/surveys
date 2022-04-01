namespace InitializeSurveySubscriber.Tests.Logic
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Google.Cloud.Functions.Testing;
    using InitializeSurveySubscriber.Logic;
    using InitializeSurveySubscriber.Model;
    using InitializeSurveySubscriber.Tests.Mocks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Newtonsoft.Json;
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
            var provider = await FunctionProviderTests.InitAsync();
            await Assert.ThrowsAsync<ArgumentNullException>(() => provider.HandleAsync(null));
        }

        /// <summary>
        ///     Initialize the provider and its dependencies.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is an <see cref="IFunctionProvider" />.</returns>
        private static async Task<IPubSubProvider<IInitializeSurveyMessage>> InitAsync()
        {
            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            var provider = new FunctionProvider(
                new MemoryLogger<Function>(),
                new PubSubMock(),
                new PubSubMock(),
                new PubSubMock(),
                new PubSubMock());
            return provider;
        }
    }
}

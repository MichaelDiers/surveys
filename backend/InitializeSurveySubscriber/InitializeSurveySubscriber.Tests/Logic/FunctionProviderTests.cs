namespace InitializeSurveySubscriber.Tests.Logic
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using InitializeSurveySubscriber.Contracts;
    using InitializeSurveySubscriber.Logic;
    using InitializeSurveySubscriber.Model;
    using InitializeSurveySubscriber.Tests.Mocks;
    using Newtonsoft.Json;
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
        /// <returns>A <see cref="Task" /> whose result is an <see cref="IFunctionProvider" />.</returns>
        private static async Task<IFunctionProvider> InitAsync()
        {
            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(await File.ReadAllTextAsync("appsettings.json"));
            var provider = new FunctionProvider(configuration, new PubSubMock(), new PubSubMock());
            return provider;
        }
    }
}

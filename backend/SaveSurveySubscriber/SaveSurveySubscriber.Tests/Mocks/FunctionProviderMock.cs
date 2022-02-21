﻿namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SaveSurveySubscriber.Contracts;
    using SaveSurveySubscriber.Logic;
    using Surveys.Common.Contracts;
    using Xunit;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProviderMock : IFunctionProvider
    {
        /// <summary>
        ///     The expected incoming message for <see cref="HandleAsync" />.
        /// </summary>
        private readonly ISaveSurveyMessage expectedMessage;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="expectedMessage">The expected incoming message for <see cref="HandleAsync" />.</param>
        public FunctionProviderMock(ISaveSurveyMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public Task HandleAsync(ISaveSurveyMessage message)
        {
            Assert.Equal(JsonConvert.SerializeObject(this.expectedMessage), JsonConvert.SerializeObject(message));

            return Task.CompletedTask;
        }
    }
}

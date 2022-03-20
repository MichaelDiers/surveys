namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts.Messages;
    using Xunit;

    internal class FunctionProviderMock : IPubSubProvider<IEvaluateSurveyMessage>
    {
        private readonly IEvaluateSurveyMessage expectedMessage;

        public FunctionProviderMock(IEvaluateSurveyMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(IEvaluateSurveyMessage message)
        {
            Assert.Equal(this.expectedMessage.InternalSurveyId, message.InternalSurveyId);
            Assert.Equal(this.expectedMessage.ProcessId, message.ProcessId);
            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}

namespace SaveSurveyResultSubscriber.Tests
{
    using Md.Common.Contracts.Model;
    using Xunit;

    public class FunctionConfigurationTests
    {
        [Theory]
        [InlineData(
            "createMailTopicName",
            "evaluateSurveyTopicName",
            Environment.Test,
            "projectId")]
        public void Ctor(
            string createMailTopicName,
            string evaluateSurveyTopicName,
            Environment environment,
            string projectId
        )
        {
            var configuration = new FunctionConfiguration
            {
                CreateMailTopicName = createMailTopicName,
                Environment = environment,
                EvaluateSurveyTopicName = evaluateSurveyTopicName,
                ProjectId = projectId
            };
            var iConfiguration = (IFunctionConfiguration) configuration;
            Assert.Equal(createMailTopicName, iConfiguration.CreateMailTopicName);
            Assert.Equal(evaluateSurveyTopicName, iConfiguration.EvaluateSurveyTopicName);
            Assert.Equal(environment, iConfiguration.Environment);
            Assert.Equal(projectId, iConfiguration.ProjectId);
        }
    }
}

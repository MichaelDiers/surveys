namespace Surveys.Common.Tests.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="SurveyStatus" />.
    /// </summary>
    public class SurveyStatusTests
    {
        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244c", Status.Created)]
        public void Ctor(string internalSurveyId, Status status)
        {
            var result = new SurveyStatus(internalSurveyId, status);
            Assert.Equal(internalSurveyId, result.InternalSurveyId);
            Assert.Equal(status, result.Status);
            Assert.True(string.IsNullOrWhiteSpace(result.ParticipantId));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244c", "bcb28b2d-e9a8-450c-a25e-7412e66d244d", Status.Created)]
        public void CtorWithParticipantId(string internalSurveyId, string participantId, Status status)
        {
            var result = new SurveyStatus(internalSurveyId, participantId, status);
            Assert.Equal(internalSurveyId, result.InternalSurveyId);
            Assert.Equal(status, result.Status);
            Assert.Equal(participantId, result.ParticipantId);
        }

        [Theory]
        [InlineData(
            "{internalSurveyId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c','participantId':null,'status':'Created'}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            null,
            Status.Created)]
        [InlineData(
            "{internalSurveyId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c','participantId':'bcb28b2d-e9a8-450c-a25e-7412e66d244d','status':'Created'}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            Status.Created)]
        public void Deserialize(
            string json,
            string internalSurveyId,
            string participantId,
            Status status
        )
        {
            var result = JsonConvert.DeserializeObject<SurveyStatus>(json);
            Assert.NotNull(result);
            Assert.Equal(internalSurveyId, result.InternalSurveyId);
            Assert.Equal(status, result.Status);
            Assert.Equal(participantId, result.ParticipantId);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new SurveyStatus(Guid.NewGuid().ToString(), Status.Created);

            var dictionary = value.ToDictionary();
            var actual = SurveyStatus.FromDictionary(dictionary);
            Assert.Equal(value.InternalSurveyId, actual.InternalSurveyId);
            Assert.Equal(value.Status, actual.Status);
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244c", "", Status.Created)]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244d", "bcb28b2d-e9a8-450c-a25e-7412e66d244e", Status.Closed)]
        public void Serialize(string internalSurveyId, string participantId, Status status)
        {
            var surveyStatus = new SurveyStatus(internalSurveyId, participantId, status);
            var expected =
                $"{{\"internalSurveyId\":\"{internalSurveyId}\",\"participantId\":\"{participantId}\",\"status\":\"{status.ToString().ToUpperInvariant()}\"}}";
            var actual = JsonConvert.SerializeObject(surveyStatus);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SurveyStatusImplementsISurveyStatus()
        {
            Assert.IsAssignableFrom<ISurveyStatus>(new SurveyStatus(Guid.NewGuid().ToString(), Status.Created));
        }
    }
}

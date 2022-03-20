namespace Surveys.Common.Tests.Models
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="SurveyResult" />.
    /// </summary>
    public class SurveyResultTests
    {
        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            true,
            "bcb28b2d-e9a8-450c-a25e-7412e66d245d",
            "bcb28b2d-e9a8-450c-a25e-7412e66d246d")]
        public void Ctor(
            string internalSurveyId,
            string participantId,
            bool isSuggested,
            string questionId,
            string choiceId
        )
        {
            var result = new SurveyResult(
                internalSurveyId,
                participantId,
                isSuggested,
                new[] {new QuestionReference(questionId, choiceId)});
            Assert.Equal(internalSurveyId, result.InternalSurveyId);
            Assert.Equal(participantId, result.ParticipantId);
            Assert.Equal(isSuggested, result.IsSuggested);
            Assert.Equal(questionId, result.Results.Single().QuestionId);
            Assert.Equal(choiceId, result.Results.Single().ChoiceId);
        }


        [Theory]
        [InlineData(
            "{internalSurveyId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c',participantId:'bcb28b2d-e9a8-450c-a25e-7412e66d244d','isSuggested':true,'results':[{'questionId':'ccb28b2d-e9a8-450c-a25e-7412e66d244d','choiceId':'dcb28b2d-e9a8-450c-a25e-7412e66d244d'}]}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            true,
            "ccb28b2d-e9a8-450c-a25e-7412e66d244d",
            "dcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Deserialize(
            string json,
            string internalSurveyId,
            string participantId,
            bool isSuggested,
            string questionId,
            string choiceId
        )
        {
            var result = JsonConvert.DeserializeObject<SurveyResult>(json);
            Assert.NotNull(result);
            Assert.Equal(internalSurveyId, result.InternalSurveyId);
            Assert.Equal(participantId, result.ParticipantId);
            Assert.Equal(isSuggested, result.IsSuggested);
            Assert.Equal(questionId, result.Results.Single().QuestionId);
            Assert.Equal(choiceId, result.Results.Single().ChoiceId);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new SurveyResult(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                true,
                new[]
                {
                    new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                });

            var dictionary = value.ToDictionary();
            var actual = SurveyResult.FromDictionary(dictionary);
            Assert.Equal(value.InternalSurveyId, actual.InternalSurveyId);
            Assert.Equal(value.IsSuggested, actual.IsSuggested);
            Assert.Equal(value.ParticipantId, actual.ParticipantId);
            Assert.Equal(value.Results.Count(), actual.Results.Count());
            Assert.True(
                value.Results.All(
                    r => actual.Results.Any(ar => r.QuestionId == ar.QuestionId && r.ChoiceId == ar.ChoiceId)));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244c", "bcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Serialize(string questionId, string choiceId)
        {
            var questionReference = new QuestionReference(questionId, choiceId);
            var expected = $"{{\"questionId\":\"{questionId}\",\"choiceId\":\"{choiceId}\"}}";
            var actual = JsonConvert.SerializeObject(questionReference);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SurveyResultImplementsISurveyResult()
        {
            Assert.IsAssignableFrom<ISurveyResult>(
                new SurveyResult(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    true,
                    new[] {new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())}));
        }
    }
}

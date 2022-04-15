namespace Surveys.Common.Tests.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="QuestionReference" />.
    /// </summary>
    public class QuestionReferenceTests
    {
        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244c", "bcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Ctor(string questionId, string choiceId)
        {
            var questionReference = new QuestionReference(questionId, choiceId);
            Assert.Equal(questionId, questionReference.QuestionId);
            Assert.Equal(choiceId, questionReference.ChoiceId);
        }

        [Theory]
        [InlineData(
            "{questionId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c',choiceId:'bcb28b2d-e9a8-450c-a25e-7412e66d244d'}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Deserialize(string json, string questionId, string choiceId)
        {
            var questionReference = JsonConvert.DeserializeObject<QuestionReference>(json);
            Assert.NotNull(questionReference);
            Assert.Equal(questionId, questionReference.QuestionId);
            Assert.Equal(choiceId, questionReference.ChoiceId);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var dictionary = value.ToDictionary();
            var actual = QuestionReference.FromDictionary(dictionary);
            Assert.Equal(value.QuestionId, actual.QuestionId);
            Assert.Equal(value.ChoiceId, actual.ChoiceId);
        }

        [Fact]
        public void QuestionReferenceImplementsIQuestionReference()
        {
            Assert.IsAssignableFrom<IQuestionReference>(
                new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
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
    }
}

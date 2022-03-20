namespace Surveys.Common.Tests.Models
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Question" />.
    /// </summary>
    public class QuestionTests
    {
        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',question:'the question',choices:[{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244c',answer:'choice answer 1',selectable:true,order:1},{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244d',answer:'choice answer 2',selectable:false,order:2}],order:1}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "the question",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "choice answer 1",
            true,
            1,
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            "choice answer 2",
            false,
            2,
            1)]
        public void Deserialize(
            string json,
            string id,
            string text,
            string choiceId1,
            string answer1,
            bool selectable1,
            int order1,
            string choiceId2,
            string answer2,
            bool selectable2,
            int order2,
            int order
        )
        {
            var question = JsonConvert.DeserializeObject<Question>(json);
            Assert.NotNull(question);
            Assert.Equal(id, question.Id);

            Assert.Equal(text, question.Text);
            Assert.Equal(choiceId1, question.Choices.First().Id);
            Assert.Equal(answer1, question.Choices.First().Answer);
            Assert.Equal(selectable1, question.Choices.First().Selectable);
            Assert.Equal(order1, question.Choices.First().Order);

            Assert.Equal(choiceId2, question.Choices.Skip(1).First().Id);
            Assert.Equal(answer2, question.Choices.Skip(1).First().Answer);
            Assert.Equal(selectable2, question.Choices.Skip(1).First().Selectable);
            Assert.Equal(order2, question.Choices.Skip(1).First().Order);

            Assert.Equal(order, question.Order);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new Question(
                Guid.NewGuid().ToString(),
                nameof(Question.Text),
                new[]
                {
                    new Choice(
                        Guid.NewGuid().ToString(),
                        nameof(Choice.Answer),
                        true,
                        1),
                    new Choice(
                        Guid.NewGuid().ToString(),
                        nameof(Choice.Answer),
                        false,
                        11)
                },
                10);

            var dictionary = value.ToDictionary();
            var actual = Question.FromDictionary(dictionary);
            Assert.Equal(value.Id, actual.Id);
            Assert.Equal(value.Order, actual.Order);
            Assert.Equal(value.Text, actual.Text);
            Assert.Equal(value.Choices.Count(), actual.Choices.Count());
            Assert.True(
                value.Choices.All(
                    c => actual.Choices.Any(
                        actualC => c.Id == actualC.Id &&
                                   c.Answer == actualC.Answer &&
                                   c.Selectable == actualC.Selectable &&
                                   c.Order == actualC.Order)));
        }

        [Fact]
        public void QuestionImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(
                new Question(
                    Guid.NewGuid().ToString(),
                    "question",
                    new[]
                    {
                        new Choice(
                            Guid.NewGuid().ToString(),
                            "answer",
                            true,
                            1)
                    },
                    1));
        }

        [Fact]
        public void QuestionImplementsIQuestion()
        {
            Assert.IsAssignableFrom<IQuestion>(
                new Question(
                    Guid.NewGuid().ToString(),
                    "question",
                    new[]
                    {
                        new Choice(
                            Guid.NewGuid().ToString(),
                            "answer",
                            true,
                            1)
                    },
                    1));
        }

        [Theory]
        [InlineData(
            "{\"id\":\"bcb28b2d-e9a8-450c-a25e-7412e66d244b\",\"question\":\"the question\",\"choices\":[{\"id\":\"bcb28b2d-e9a8-450c-a25e-7412e66d244c\",\"answer\":\"choice answer 1\",\"selectable\":true,\"order\":1},{\"id\":\"bcb28b2d-e9a8-450c-a25e-7412e66d244d\",\"answer\":\"choice answer 2\",\"selectable\":false,\"order\":2}],\"order\":1}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "the question",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "choice answer 1",
            true,
            1,
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            "choice answer 2",
            false,
            2,
            1)]
        public void Serialize(
            string json,
            string id,
            string text,
            string choiceId1,
            string answer1,
            bool selectable1,
            int order1,
            string choiceId2,
            string answer2,
            bool selectable2,
            int order2,
            int order
        )
        {
            var question = new Question(
                id,
                text,
                new[]
                {
                    new Choice(
                        choiceId1,
                        answer1,
                        selectable1,
                        order1),
                    new Choice(
                        choiceId2,
                        answer2,
                        selectable2,
                        order2)
                },
                order);
            var actual = JsonConvert.SerializeObject(question);
            Assert.Equal(json, actual);
        }
    }
}

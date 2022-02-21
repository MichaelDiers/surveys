namespace Surveys.Common.Tests.Models
{
    using System;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Survey" />.
    /// </summary>
    public class SurveyTests
    {
        [Fact]
        public void DeserializeObject()
        {
            var json = new FileInfo("Models/SurveyTests.Serialize.json").OpenText().ReadToEnd();
            var _ = JsonConvert.DeserializeObject<Survey>(json);
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

        [Fact]
        public void SerializeObject()
        {
            var questions = new[]
            {
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
                    10)
            };

            var survey = new Survey(
                Guid.NewGuid().ToString(),
                "The survey",
                "The info",
                "http://www.google.de",
                new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                new[]
                {
                    new Participant(
                        Guid.NewGuid().ToString(),
                        "foobar@example.example",
                        "name",
                        new[]
                        {
                            new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                        },
                        1)
                },
                questions);
            var _ = JsonConvert.SerializeObject(survey);
        }


        [Fact]
        public void SurveyImplementsIBase()
        {
            var questions = new[]
            {
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
                    10)
            };

            Assert.IsAssignableFrom<IBase>(
                new Survey(
                    Guid.NewGuid().ToString(),
                    "The survey",
                    "The info",
                    "http://www.google.de",
                    new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                    new[]
                    {
                        new Participant(
                            Guid.NewGuid().ToString(),
                            "foobar@example.example",
                            "name",
                            new[]
                            {
                                new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                            },
                            1)
                    },
                    questions));
        }


        [Fact]
        public void SurveyImplementsISurvey()
        {
            var questions = new[]
            {
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
                    10)
            };

            Assert.IsAssignableFrom<ISurvey>(
                new Survey(
                    Guid.NewGuid().ToString(),
                    "The survey",
                    "The info",
                    "http://www.google.de",
                    new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                    new[]
                    {
                        new Participant(
                            Guid.NewGuid().ToString(),
                            "foobar@example.example",
                            "name",
                            new[]
                            {
                                new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                            },
                            1)
                    },
                    questions));
        }

        [Fact]
        public void ToDictionary()
        {
            var json = new FileInfo("Models/SurveyTests.Serialize.json").OpenText().ReadToEnd();
            var survey = JsonConvert.DeserializeObject<Survey>(json);
            Assert.NotNull(survey);
            var _ = survey.ToDictionary();
        }
    }
}

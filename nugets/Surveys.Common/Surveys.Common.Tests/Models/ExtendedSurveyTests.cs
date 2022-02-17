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
    ///     Tests for <see cref="ExtendedSurvey" />.
    /// </summary>
    public class ExtendedSurveyTests
    {
        [Fact]
        public void DeserializeObject()
        {
            var json = new FileInfo("Models/ExtendedSurveyTests.Serialize.json").OpenText().ReadToEnd();
            var _ = JsonConvert.DeserializeObject<ExtendedSurvey>(json);
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
                        new Choice(Guid.NewGuid().ToString(), "answer", true)
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
                        new Choice(Guid.NewGuid().ToString(), "answer", true)
                    },
                    10)
            };

            var survey = new ExtendedSurvey(
                Guid.NewGuid().ToString(),
                "The survey",
                "The info",
                "http://www.google.de",
                new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                new[]
                {
                    new ExtendedParticipant(
                        Guid.NewGuid().ToString(),
                        "foobar@example.example",
                        "name",
                        new[]
                        {
                            new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                        })
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
                        new Choice(Guid.NewGuid().ToString(), "answer", true)
                    },
                    10)
            };

            Assert.IsAssignableFrom<IBase>(
                new ExtendedSurvey(
                    Guid.NewGuid().ToString(),
                    "The survey",
                    "The info",
                    "http://www.google.de",
                    new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                    new[]
                    {
                        new ExtendedParticipant(
                            Guid.NewGuid().ToString(),
                            "foobar@example.example",
                            "name",
                            new[]
                            {
                                new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                            })
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
                        new Choice(Guid.NewGuid().ToString(), "answer", true)
                    },
                    10)
            };

            Assert.IsAssignableFrom<IExtendedSurvey>(
                new ExtendedSurvey(
                    Guid.NewGuid().ToString(),
                    "The survey",
                    "The info",
                    "http://www.google.de",
                    new Person(Guid.NewGuid().ToString(), "my@foo.example", "foobar"),
                    new[]
                    {
                        new ExtendedParticipant(
                            Guid.NewGuid().ToString(),
                            "foobar@example.example",
                            "name",
                            new[]
                            {
                                new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)
                            })
                    },
                    questions));
        }
    }
}

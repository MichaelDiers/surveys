namespace Surveys.Common.Tests.Models
{
    using System;
    using System.IO;
    using System.Linq;
    using Md.Common.Contracts.Database;
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
        public void FromDictionary()
        {
            var json = new FileInfo("Models/SurveyTests.Serialize.json").OpenText().ReadToEnd();
            var value = JsonConvert.DeserializeObject<Survey>(json);
            Assert.NotNull(value);

            var dictionary = value.ToDictionary();
            var actual = Survey.FromDictionary(dictionary);
            Assert.Equal(value.DocumentId, actual.DocumentId);
            Assert.Equal(value.Created, actual.Created);
            Assert.Equal(value.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(value.Name, actual.Name);
            Assert.Equal(value.Info, actual.Info);
            Assert.Equal(value.Link, actual.Link);
            Assert.Equal(value.Questions.Count(), actual.Questions.Count());
            Assert.True(
                value.Questions.All(
                    q => actual.Questions.Any(
                        aq => q.Id == aq.Id &&
                              q.Order == aq.Order &&
                              q.Text == aq.Text &&
                              q.Choices.All(
                                  c => aq.Choices.Any(
                                      ac => c.Selectable == ac.Selectable &&
                                            c.Answer == ac.Answer &&
                                            c.Id == ac.Id &&
                                            c.Order == ac.Order)))));
            Assert.Equal(value.Participants.Count(), actual.Participants.Count());
            Assert.True(
                value.Participants.All(
                    p => actual.Participants.Any(
                        ap => p.Email == ap.Email &&
                              p.Id == ap.Id &&
                              p.Name == ap.Name &&
                              p.Order == ap.Order &&
                              p.QuestionReferences.All(
                                  qr => ap.QuestionReferences.Any(
                                      aqr => qr.ChoiceId == aqr.ChoiceId && qr.QuestionId == aqr.QuestionId)))));
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
                DateTime.Now,
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
                        new[] {new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)},
                        1)
                },
                questions);
            var _ = JsonConvert.SerializeObject(survey);
        }

        [Fact]
        public void SurveyImplementsIDatabaseObject()
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

            Assert.IsAssignableFrom<IDatabaseObject>(
                new Survey(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
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
                            new[] {new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)},
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
                    DateTime.Now,
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
                            new[] {new QuestionReference(questions.First().Id, questions.First().Choices.First().Id)},
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

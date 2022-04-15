namespace Surveys.Common.Tests.Models
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Participant" />.
    /// </summary>
    public class ParticipantTests
    {
        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            1)]
        public void Ctor(
            string id,
            string email,
            string name,
            string questionId,
            string choiceId,
            int order
        )
        {
            var participant = new Participant(
                id,
                email,
                name,
                new[] {new QuestionReference(questionId, choiceId)},
                order);
            Assert.Equal(id, participant.Id);
            Assert.Equal(email, participant.Email);
            Assert.Equal(name, participant.Name);
            Assert.Equal(order, participant.Order);
        }

        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',email:'foo@bar.example',name:'the name',questionReferences:[{questionId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c',choiceId:'bcb28b2d-e9a8-450c-a25e-7412e66d244d'}],order:1}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            1)]
        public void Deserialize(
            string json,
            string id,
            string email,
            string name,
            string questionId,
            string choiceId,
            int order
        )
        {
            var participant = JsonConvert.DeserializeObject<Participant>(json);
            Assert.NotNull(participant);
            Assert.Equal(id, participant.Id);
            Assert.Equal(email, participant.Email);
            Assert.Equal(name, participant.Name);
            Assert.Equal(questionId, participant.QuestionReferences.Single().QuestionId);
            Assert.Equal(choiceId, participant.QuestionReferences.Single().ChoiceId);
            Assert.Equal(order, participant.Order);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new Participant(
                Guid.NewGuid().ToString(),
                "email@example.example",
                nameof(Person.Name),
                new[]
                {
                    new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                },
                10);

            var dictionary = value.ToDictionary();
            var actual = Participant.FromDictionary(dictionary);
            Assert.Equal(value.Id, actual.Id);
            Assert.Equal(value.Order, actual.Order);
            Assert.Equal(value.Email, actual.Email);
            Assert.Equal(value.Name, actual.Name);
            Assert.Equal(value.QuestionReferences.Count(), actual.QuestionReferences.Count());
            Assert.True(
                value.QuestionReferences.All(
                    qr => actual.QuestionReferences.Any(
                        actualQr => qr.QuestionId == actualQr.QuestionId && qr.ChoiceId == actualQr.ChoiceId)));
        }

        [Fact]
        public void ParticipantImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(
                new Participant(
                    Guid.NewGuid().ToString(),
                    "foo@bar.example",
                    "foo",
                    new[] {new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())},
                    1));
        }

        [Fact]
        public void ParticipantImplementsIParticipant()
        {
            Assert.IsAssignableFrom<IParticipant>(
                new Participant(
                    Guid.NewGuid().ToString(),
                    "foo@bar.example",
                    "foo",
                    new[] {new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())},
                    1));
        }

        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244e",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244f",
            1)]
        public void Serialize(
            string id,
            string email,
            string name,
            string questionId1,
            string choiceId1,
            string questionId2,
            string choiceId2,
            int order
        )
        {
            var participant = new Participant(
                id,
                email,
                name,
                new[] {new QuestionReference(questionId1, choiceId1), new QuestionReference(questionId2, choiceId2)},
                order);
            var expected =
                $"{{\"id\":\"{id}\",\"email\":\"{email}\",\"name\":\"{name}\",\"questionReferences\":[{{\"questionId\":\"{questionId1}\",\"choiceId\":\"{choiceId1}\"}},{{\"questionId\":\"{questionId2}\",\"choiceId\":\"{choiceId2}\"}}],\"order\":{order}}}";
            var actual = JsonConvert.SerializeObject(participant);
            Assert.Equal(expected, actual);
        }
    }
}

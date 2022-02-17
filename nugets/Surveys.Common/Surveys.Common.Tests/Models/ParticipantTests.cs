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
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Ctor(
            string id,
            string email,
            string name,
            string questionId,
            string choiceId
        )
        {
            var participant = new Participant(
                id,
                email,
                name,
                new[]
                {
                    new QuestionReference(questionId, choiceId)
                });
            Assert.Equal(id, participant.Id);
            Assert.Equal(email, participant.Email);
            Assert.Equal(name, participant.Name);
        }


        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',email:'foo@bar.example',name:'the name',questionReferences:[{questionId:'bcb28b2d-e9a8-450c-a25e-7412e66d244c',choiceId:'bcb28b2d-e9a8-450c-a25e-7412e66d244d'}]}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d")]
        public void Deserialize(
            string json,
            string id,
            string email,
            string name,
            string questionId,
            string choiceId
        )
        {
            var participant = JsonConvert.DeserializeObject<Participant>(json);
            Assert.NotNull(participant);
            Assert.Equal(id, participant.Id);
            Assert.Equal(email, participant.Email);
            Assert.Equal(name, participant.Name);
            Assert.Equal(questionId, participant.QuestionReferences.Single().QuestionId);
            Assert.Equal(choiceId, participant.QuestionReferences.Single().ChoiceId);
        }

        [Fact]
        public void ParticipantImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(
                new Participant(
                    Guid.NewGuid().ToString(),
                    "foo@bar.example",
                    "foo",
                    new[]
                    {
                        new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                    }));
        }

        [Fact]
        public void ParticipantImplementsIParticipant()
        {
            Assert.IsAssignableFrom<IParticipant>(
                new Participant(
                    Guid.NewGuid().ToString(),
                    "foo@bar.example",
                    "foo",
                    new[]
                    {
                        new QuestionReference(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                    }));
        }

        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244e",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244f")]
        public void Serialize(
            string id,
            string email,
            string name,
            string questionId1,
            string choiceId1,
            string questionId2,
            string choiceId2
        )
        {
            var participant = new Participant(
                id,
                email,
                name,
                new[]
                {
                    new QuestionReference(questionId1, choiceId1),
                    new QuestionReference(questionId2, choiceId2)
                });
            var expected =
                $"{{\"id\":\"{id}\",\"email\":\"{email}\",\"name\":\"{name}\",\"questionReferences\":[{{\"questionId\":\"{questionId1}\",\"choiceId\":\"{choiceId1}\"}},{{\"questionId\":\"{questionId2}\",\"choiceId\":\"{choiceId2}\"}}]}}";
            var actual = JsonConvert.SerializeObject(participant);
            Assert.Equal(expected, actual);
        }
    }
}

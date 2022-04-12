namespace Surveys.Common.Tests.Models
{
    using System;
    using Md.Common.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="SurveyStatus" />.
    /// </summary>
    public class SurveyStatusTests
    {
        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244d", "bcb28b2d-e9a8-450c-a25e-7412e66d244c", Status.Created)]
        public void Ctor(string documentId, string parentDocumentId, Status status)
        {
            var created = DateTime.Now;
            var result = new SurveyStatus(
                documentId,
                created,
                parentDocumentId,
                status);
            Assert.Equal(documentId, result.DocumentId);
            Assert.Equal(created, result.Created);
            Assert.Equal(parentDocumentId, result.ParentDocumentId);
            Assert.Equal(string.Empty, result.ParticipantId);
            Assert.Equal(status, result.Status);
            Assert.True(string.IsNullOrWhiteSpace(result.ParticipantId));
        }

        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244d",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244c",
            Status.Created)]
        public void CtorWithParticipantId(
            string documentId,
            string parentDocumentId,
            string participantId,
            Status status
        )
        {
            var created = DateTime.Now;
            var result = new SurveyStatus(
                documentId,
                created,
                parentDocumentId,
                participantId,
                status);
            Assert.Equal(documentId, result.DocumentId);
            Assert.Equal(created, result.Created);
            Assert.Equal(parentDocumentId, result.ParentDocumentId);
            Assert.Equal(participantId, result.ParticipantId);
            Assert.Equal(status, result.Status);
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new SurveyStatus(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Status.Created);

            var dictionary = value.ToDictionary();
            var actual = SurveyStatus.FromDictionary(dictionary);
            Assert.Equal(value.DocumentId, actual.DocumentId);
            Assert.Equal(value.Created, actual.Created);
            Assert.Equal(value.ParentDocumentId, actual.ParentDocumentId);

            Assert.Equal(value.ParticipantId, actual.ParticipantId);
            Assert.Equal(value.Status, actual.Status);
        }

        [Fact]
        public void Serialize()
        {
            var expected = new SurveyStatus(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Status.Created);
            var actual = Serializer.DeserializeObject<SurveyStatus>(Serializer.SerializeObject(expected));
            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);

            Assert.Equal(expected.ParticipantId, actual.ParticipantId);
            Assert.Equal(expected.Status, actual.Status);
        }

        [Fact]
        public void SurveyStatusImplementsISurveyStatus()
        {
            Assert.IsAssignableFrom<ISurveyStatus>(
                new SurveyStatus(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Status.Created));
        }
    }
}

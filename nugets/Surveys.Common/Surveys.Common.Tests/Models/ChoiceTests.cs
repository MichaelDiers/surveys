namespace Surveys.Common.Tests.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Choice" />.
    /// </summary>
    public class ChoiceTests
    {
        [Fact]
        public void ChoiceImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(new Choice(Guid.NewGuid().ToString(), "answer", true));
        }

        [Fact]
        public void ChoiceImplementsIChoice()
        {
            Assert.IsAssignableFrom<IChoice>(new Choice(Guid.NewGuid().ToString(), "answer", true));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b", "the answer", true)]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244a", "the other answer", false)]
        public void Ctor(string id, string answer, bool selectable)
        {
            var baseObject = new Choice(id, answer, selectable);
            Assert.Equal(id, baseObject.Id);
            Assert.Equal(answer, baseObject.Answer);
            Assert.Equal(selectable, baseObject.Selectable);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ctor_ThrowsArgumentException_Answer(string answer)
        {
            Assert.Throws<ArgumentException>(() => new Choice("bcb28b2d-e9a8-450c-a25e-7412e66d244b", answer, true));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244")]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244ba")]
        public void Ctor_ThrowsArgumentException_Id(string id)
        {
            Assert.Throws<ArgumentException>(() => new Choice(id, "answer", true));
        }

        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',answer:'my answer',selectable:true}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "my answer",
            true)]
        public void Deserialize(
            string json,
            string id,
            string answer,
            bool selectable
        )
        {
            var choice = JsonConvert.DeserializeObject<Choice>(json);
            Assert.NotNull(choice);
            Assert.Equal(id, choice.Id);
            Assert.Equal(answer, choice.Answer);
            Assert.Equal(selectable, choice.Selectable);
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{answer:'answer',selectable:true}")]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',selectable:true}")]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244',answer:'my answer',selectable:true}")]
        public void Deserialize_ThrowsArgumentException(string json)
        {
            Assert.Throws<ArgumentException>(() => JsonConvert.DeserializeObject<Choice>(json));
        }

        [Theory]
        [InlineData(null)]
        public void Deserialize_ThrowsArgumentNullException(string json)
        {
            Assert.Throws<ArgumentNullException>(() => JsonConvert.DeserializeObject<Choice>(json));
        }

        [Theory]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',answer:'answer',selectable:'something'}")]
        public void Deserialize_ThrowsJsonReaderException(string json)
        {
            Assert.Throws<JsonReaderException>(() => JsonConvert.DeserializeObject<Choice>(json));
        }

        [Theory]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',answer:'answer'}")]
        public void Deserialize_ThrowsJsonSerializationException(string json)
        {
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<Choice>(json));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b", "the answer", true)]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b", "the answer", false)]
        public void Serialize(string id, string answer, bool selectable)
        {
            var choice = new Choice(id, answer, selectable);
            var expected =
                $"{{\"id\":\"{id}\",\"answer\":\"{answer}\",\"selectable\":{selectable.ToString().ToLower()}}}";
            var actual = JsonConvert.SerializeObject(choice);
            Assert.Equal(expected, actual);
        }
    }
}

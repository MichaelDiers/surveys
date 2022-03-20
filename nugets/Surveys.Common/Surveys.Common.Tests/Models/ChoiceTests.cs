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
            Assert.IsAssignableFrom<IBase>(
                new Choice(
                    Guid.NewGuid().ToString(),
                    "answer",
                    true,
                    1));
        }

        [Fact]
        public void ChoiceImplementsIChoice()
        {
            Assert.IsAssignableFrom<IChoice>(
                new Choice(
                    Guid.NewGuid().ToString(),
                    "answer",
                    true,
                    1));
        }

        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "the answer",
            true,
            1)]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244a",
            "the other answer",
            false,
            -1)]
        public void Ctor(
            string id,
            string answer,
            bool selectable,
            int order
        )
        {
            var baseObject = new Choice(
                id,
                answer,
                selectable,
                order);
            Assert.Equal(id, baseObject.Id);
            Assert.Equal(answer, baseObject.Answer);
            Assert.Equal(selectable, baseObject.Selectable);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ctor_ThrowsArgumentException_Answer(string answer)
        {
            Assert.Throws<ArgumentException>(
                () => new Choice(
                    "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
                    answer,
                    true,
                    1));
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
            Assert.Throws<ArgumentException>(
                () => new Choice(
                    id,
                    "answer",
                    true,
                    1));
        }

        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',answer:'my answer',selectable:true,order:1}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "my answer",
            true,
            1)]
        public void Deserialize(
            string json,
            string id,
            string answer,
            bool selectable,
            int order
        )
        {
            var choice = JsonConvert.DeserializeObject<Choice>(json);
            Assert.NotNull(choice);
            Assert.Equal(id, choice.Id);
            Assert.Equal(answer, choice.Answer);
            Assert.Equal(selectable, choice.Selectable);
            Assert.Equal(order, choice.Order);
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

        [Fact]
        public void FromDictionary()
        {
            var value = new Choice(
                Guid.NewGuid().ToString(),
                nameof(Choice.Answer),
                true,
                10);
            var dictionary = value.ToDictionary();
            var actual = Choice.FromDictionary(dictionary);
            Assert.Equal(value.Id, actual.Id);
            Assert.Equal(value.Order, actual.Order);
            Assert.Equal(value.Answer, actual.Answer);
            Assert.Equal(value.Selectable, actual.Selectable);
        }

        [Theory]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "the answer",
            true,
            1)]
        [InlineData(
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "the answer",
            false,
            -1)]
        public void Serialize(
            string id,
            string answer,
            bool selectable,
            int order
        )
        {
            var choice = new Choice(
                id,
                answer,
                selectable,
                order);
            var expected =
                $"{{\"id\":\"{id}\",\"answer\":\"{answer}\",\"selectable\":{selectable.ToString().ToLower()},\"order\":{order}}}";
            var actual = JsonConvert.SerializeObject(choice);
            Assert.Equal(expected, actual);
        }
    }
}

namespace Surveys.Common.Tests.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Base" />.
    /// </summary>
    public class BaseTests
    {
        [Fact]
        public void BaseImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(new Base(Guid.NewGuid().ToString()));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b")]
        public void Ctor(string id)
        {
            var baseObject = new Base(id);
            Assert.Equal(id, baseObject.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244")]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244ba")]
        public void Ctor_ThrowsArgumentException(string id)
        {
            Assert.Throws<ArgumentException>(() => new Base(id));
        }

        [Theory]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b'}", "bcb28b2d-e9a8-450c-a25e-7412e66d244b")]
        public void Deserialize(string json, string id)
        {
            var baseObject = JsonConvert.DeserializeObject<Base>(json);
            Assert.NotNull(baseObject);
            Assert.Equal(id, baseObject.Id);
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{id:''}")]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244'}")]
        [InlineData("{id:'1'}")]
        [InlineData("{id:'a'}")]
        [InlineData("{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244ba'}")]
        public void Deserialize_ThrowsArgumentException(string json)
        {
            Assert.Throws<ArgumentException>(() => JsonConvert.DeserializeObject<Base>(json));
        }

        [Fact]
        public void FromDictionary()
        {
            var value = new Base(Guid.NewGuid().ToString());
            var dictionary = value.ToDictionary();
            var actual = Base.FromDictionary(dictionary);
            Assert.Equal(value.Id, actual.Id);
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b")]
        public void Serialize(string id)
        {
            var baseData = new Base(id);
            var json = JsonConvert.SerializeObject(baseData);
            Assert.Equal($"{{\"id\":\"{id}\"}}", json);
        }
    }
}

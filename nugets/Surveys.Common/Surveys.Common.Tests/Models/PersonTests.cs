namespace Surveys.Common.Tests.Models
{
    using System;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Person" />.
    /// </summary>
    public class PersonTests
    {
        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b", "foo@bar.example", "the name")]
        public void Ctor(string id, string email, string name)
        {
            var person = new Person(id, email, name);
            Assert.Equal(id, person.Id);
            Assert.Equal(email, person.Email);
            Assert.Equal(name, person.Name);
        }


        [Theory]
        [InlineData(
            "{id:'bcb28b2d-e9a8-450c-a25e-7412e66d244b',email:'foo@bar.example',name:'the name'}",
            "bcb28b2d-e9a8-450c-a25e-7412e66d244b",
            "foo@bar.example",
            "the name")]
        public void Deserialize(
            string json,
            string id,
            string email,
            string name
        )
        {
            var person = JsonConvert.DeserializeObject<Person>(json);
            Assert.NotNull(person);
            Assert.Equal(id, person.Id);
            Assert.Equal(email, person.Email);
            Assert.Equal(name, person.Name);
        }

        [Fact]
        public void PersonImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(new Person(Guid.NewGuid().ToString(), "foo@bar.example", "foo"));
        }

        [Fact]
        public void PersonImplementsIPerson()
        {
            Assert.IsAssignableFrom<IPerson>(new Person(Guid.NewGuid().ToString(), "foo@bar.example", "foo"));
        }

        [Theory]
        [InlineData("bcb28b2d-e9a8-450c-a25e-7412e66d244b", "foo@bar.example", "the name")]
        public void Serialize(string id, string email, string name)
        {
            var person = new Person(id, email, name);
            var expected = $"{{\"id\":\"{id}\",\"email\":\"{email}\",\"name\":\"{name}\"}}";
            var actual = JsonConvert.SerializeObject(person);
            Assert.Equal(expected, actual);
        }
    }
}

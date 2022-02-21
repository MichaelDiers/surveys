﻿namespace Surveys.Common.Tests.Extensions
{
    using Surveys.Common.Extensions;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="StringExtensions" />.
    /// </summary>
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("ab", "ab")]
        [InlineData("Ab", "ab")]
        [InlineData("AB", "aB")]
        [InlineData("A", "a")]
        [InlineData("a", "a")]
        public void FirstCharacterToLower(string input, string expected)
        {
            Assert.Equal(expected, input.FirstCharacterToLower());
        }
    }
}

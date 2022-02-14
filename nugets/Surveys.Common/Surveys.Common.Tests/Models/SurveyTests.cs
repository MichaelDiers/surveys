namespace Surveys.Common.Tests.Models
{
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Survey" />.
    /// </summary>
    public class SurveyTests
    {
        /// <summary>
        ///     Check if <see cref="Survey" /> implements <see cref="ISurvey" />.
        /// </summary>
        [Fact]
        public void SurveyIsInstanceOfISurvey()
        {
            var survey = new Survey();
            Assert.IsAssignableFrom<ISurvey>(survey);
        }
    }
}

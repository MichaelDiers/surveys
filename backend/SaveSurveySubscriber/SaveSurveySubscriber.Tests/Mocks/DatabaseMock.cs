namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using SaveSurveySubscriber.Contracts;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Mock for databases.
    /// </summary>
    internal class DatabaseMock : IDatabase
    {
        /// <summary>
        ///     Insert a new survey.
        /// </summary>
        /// <param name="message">The survey data.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public Task Insert(ISaveSurveyMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

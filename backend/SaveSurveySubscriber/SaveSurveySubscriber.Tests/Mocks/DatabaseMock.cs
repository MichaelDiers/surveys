namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using SaveSurveySubscriber.Contracts;

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
        public Task Insert(IMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

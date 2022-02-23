namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Mock for databases.
    /// </summary>
    internal class DatabaseMock : IDatabase
    {
        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public Task InsertAsync(string documentId, IDictionaryConverter data)
        {
            return Task.CompletedTask;
        }

        public Task InsertAsync(IDictionaryConverter data)
        {
            return Task.CompletedTask;
        }
    }
}

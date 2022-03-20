namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Mock for databases.
    /// </summary>
    internal class DatabaseMock : IDatabase
    {
        public Task InsertAsync(string documentId, IToDictionary data)
        {
            return Task.CompletedTask;
        }

        public Task InsertAsync(IToDictionary data)
        {
            return Task.CompletedTask;
        }

        public Task<IDictionary<string, object>?> ReadByDocumentIdAsync(string documentId)
        {
            return Task.FromResult<IDictionary<string, object>>(new Dictionary<string, object>());
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(string fieldPath, object value)
        {
            return Task.FromResult(Enumerable.Empty<IDictionary<string, object>>());
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            return Task.FromResult(Enumerable.Empty<IDictionary<string, object>>());
        }

        public Task<IDictionary<string, object>?> ReadOneAsync(string fieldPath, object value)
        {
            return Task.FromResult<IDictionary<string, object>?>(null);
        }
    }
}

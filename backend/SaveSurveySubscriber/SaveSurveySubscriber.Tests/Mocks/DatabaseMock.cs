namespace SaveSurveySubscriber.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Mock for databases.
    /// </summary>
    internal class DatabaseMock : ISurveyDatabase
    {
        public Task<string> InsertAsync(string documentId, IToDictionary data)
        {
            return null;
        }

        public Task<string> InsertAsync(IToDictionary data)
        {
            return null;
        }

        public Task UpdateByDocumentIdAsync(string documentId, IDictionary<string, object> updates)
        {
            return null;
        }

        public Task UpdateOneAsync(string fieldPath, object value, IDictionary<string, object> updates)
        {
            return null;
        }

        public Task<ISurvey?> ReadByDocumentIdAsync(string documentId)
        {
            return null;
        }

        public Task<IEnumerable<ISurvey>> ReadManyAsync(string fieldPath, object value)
        {
            return null;
        }

        public Task<IEnumerable<ISurvey>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            return null;
        }

        public Task<ISurvey?> ReadOneAsync(string fieldPath, object value)
        {
            return null;
        }
    }
}

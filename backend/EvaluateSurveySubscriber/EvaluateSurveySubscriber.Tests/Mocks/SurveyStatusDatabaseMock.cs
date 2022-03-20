namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Contracts;
    using EvaluateSurveySubscriber.Tests.Data;
    using Md.GoogleCloud.Base.Contracts.Logic;

    internal class SurveyStatusDatabaseMock : ISurveyStatusDatabase
    {
        private readonly bool isClosed;

        public SurveyStatusDatabaseMock(bool isClosed)
        {
            this.isClosed = isClosed;
        }

        public Task<IDictionary<string, object>?> ReadByDocumentIdAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(string fieldPath, object value)
        {
            return await this.ReadManyAsync(fieldPath, value, OrderType.Unsorted);
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            return Task.FromResult(TestData.CreateStatus((string) value, this.isClosed).Select(x => x.ToDictionary()));
        }

        public Task<IDictionary<string, object>?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}

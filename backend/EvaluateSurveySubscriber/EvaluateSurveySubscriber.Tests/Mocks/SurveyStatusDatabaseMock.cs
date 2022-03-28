namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Tests.Data;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    internal class SurveyStatusDatabaseMock : ISurveyStatusReadOnlyDatabase
    {
        private readonly bool isClosed;

        public SurveyStatusDatabaseMock(bool isClosed)
        {
            this.isClosed = isClosed;
        }

        public Task<ISurveyStatus?> ReadByDocumentIdAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ISurveyStatus>> ReadManyAsync(string fieldPath, object value)
        {
            return await this.ReadManyAsync(fieldPath, value, OrderType.Unsorted);
        }

        public Task<IEnumerable<ISurveyStatus>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            return Task.FromResult(TestData.CreateStatus((string) value, this.isClosed));
        }

        public Task<ISurveyStatus?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}

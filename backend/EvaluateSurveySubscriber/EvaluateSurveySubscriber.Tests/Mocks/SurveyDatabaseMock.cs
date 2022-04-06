namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Tests.Data;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    internal class SurveyDatabaseMock : ISurveyReadOnlyDatabase
    {
        public Task<ISurvey> ReadByDocumentIdAsync(string documentId)
        {
            return Task.FromResult(TestData.CreateSurvey());
        }

        public Task<IEnumerable<ISurvey>> ReadManyAsync()
        {
            return null;
        }

        public Task<IEnumerable<ISurvey>> ReadManyAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISurvey>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            throw new NotImplementedException();
        }

        public Task<ISurvey?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}

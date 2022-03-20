namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Contracts;
    using EvaluateSurveySubscriber.Tests.Data;
    using Md.GoogleCloud.Base.Contracts.Logic;

    internal class SurveyDatabaseMock : ISurveyDatabase
    {
        public Task<IDictionary<string, object>?> ReadByDocumentIdAsync(string documentId)
        {
            var survey = TestData.CreateSurvey();
            return Task.FromResult(survey.ToDictionary());
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}

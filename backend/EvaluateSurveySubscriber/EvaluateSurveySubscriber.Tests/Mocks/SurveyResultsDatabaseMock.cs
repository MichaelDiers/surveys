namespace EvaluateSurveySubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EvaluateSurveySubscriber.Tests.Data;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    internal class SurveyResultsDatabaseMock : ISurveyResultReadOnlyDatabase
    {
        private readonly bool allVoted;

        public SurveyResultsDatabaseMock(bool allVoted)
        {
            this.allVoted = allVoted;
        }

        public Task<ISurveyResult?> ReadByDocumentIdAsync(string documentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISurveyResult>> ReadManyAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISurveyResult>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            return Task.FromResult(TestData.CreateResults((string) value, this.allVoted));
        }

        public Task<ISurveyResult?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}

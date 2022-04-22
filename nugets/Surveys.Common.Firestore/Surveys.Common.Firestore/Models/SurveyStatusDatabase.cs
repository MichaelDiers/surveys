namespace Surveys.Common.Firestore.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurveyStatus" />.
    /// </summary>
    public class SurveyStatusDatabase : Database<ISurveyStatus>, ISurveyStatusDatabase
    {
        /// <summary>
        ///     The base name of the collection.
        /// </summary>
        public const string CollectionNameBase = "survey-status";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatusDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyStatusDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyStatusDatabase.CollectionNameBase, SurveyStatus.FromDictionary)
        {
        }

        public async Task<string?> InsertIfNotExistsAsync(ISurveyStatus surveyStatus)
        {
            var documentData = surveyStatus.ToDictionary();

            var _ = documentData.Remove(DatabaseObject.DocumentIdName);
            if (!documentData.TryAdd(DatabaseObject.CreatedName, FieldValue.ServerTimestamp))
            {
                documentData[DatabaseObject.CreatedName] = FieldValue.ServerTimestamp;
            }

            var documentId = Guid.NewGuid().ToString();
            var documentReference = this.Collection().Document(documentId);

            var created = false;
            await this.Collection()
                .Database.RunTransactionAsync(
                    async transaction =>
                    {
                        var query = this.Collection()
                            .WhereEqualTo(DatabaseObject.ParentDocumentIdName, surveyStatus.ParentDocumentId)
                            .WhereEqualTo(SurveyStatus.StatusName, Status.Closed.ToString())
                            .Limit(1);
                        var snapshot = await transaction.GetSnapshotAsync(query);
                        if (snapshot.Count == 0)
                        {
                            transaction.Create(documentReference, documentData);
                            created = true;
                        }
                    });

            return created ? documentId : null;
        }
    }
}

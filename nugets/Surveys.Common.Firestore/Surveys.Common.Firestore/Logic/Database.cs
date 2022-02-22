namespace Surveys.Common.Firestore.Logic
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public class Database : IDatabase
    {
        /// <summary>
        ///     Access to the database implementation.
        /// </summary>
        private readonly FirestoreDb database;

        /// <summary>
        ///     Configuration of the database.
        /// </summary>
        private readonly IDatabaseConfiguration databaseConfiguration;

        /// <summary>
        ///     Creates a new instance of <see cref="Database" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        public Database(IDatabaseConfiguration databaseConfiguration)
        {
            this.databaseConfiguration =
                databaseConfiguration ?? throw new ArgumentNullException(nameof(databaseConfiguration));
            this.database = FirestoreDb.Create(databaseConfiguration.ProjectId);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InsertAsync(string documentId, IDictionaryConverter data)
        {
            var documentReference =
                this.database.Collection(this.databaseConfiguration.CollectionName).Document(documentId);
            await this.InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InsertAsync(IDictionaryConverter data)
        {
            var documentReference = this.database.Collection(this.databaseConfiguration.CollectionName).Document();
            await this.InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentReference">The document reference used for inserting.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        private async Task InsertAsync(DocumentReference documentReference, IDictionaryConverter data)
        {
            var document = data.ToDictionary();
            document.Add("created", FieldValue.ServerTimestamp);
            await documentReference.CreateAsync(document);
        }
    }
}

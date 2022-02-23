namespace Surveys.Common.Firestore.Logic
{
    using System;
    using Google.Cloud.Firestore;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Base class for database access.
    /// </summary>
    public abstract class DatabaseBase
    {
        /// <summary>
        ///     Gets the configuration of the database.
        /// </summary>
        private readonly IDatabaseConfiguration databaseConfiguration;

        /// <summary>
        ///     Access to the database implementation.
        /// </summary>
        private readonly FirestoreDb firestoreDb;

        /// <summary>
        ///     Creates a new instance of <see cref="Database" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        protected DatabaseBase(IDatabaseConfiguration databaseConfiguration)
        {
            this.databaseConfiguration =
                databaseConfiguration ?? throw new ArgumentNullException(nameof(databaseConfiguration));
            this.firestoreDb = FirestoreDb.Create(databaseConfiguration.ProjectId);
        }

        /// <summary>
        ///     Gets a reference to the database collection.
        /// </summary>
        /// <returns>A <see cref="CollectionReference" />.</returns>
        protected CollectionReference Collection()
        {
            return this.firestoreDb.Collection(this.databaseConfiguration.CollectionName);
        }
    }
}

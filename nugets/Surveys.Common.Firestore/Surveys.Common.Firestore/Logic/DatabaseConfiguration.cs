namespace Surveys.Common.Firestore.Logic
{
    using System;

    /// <summary>
    ///     Configuration of the database.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the google cloud project.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public DatabaseConfiguration(string projectId, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectId));
            }

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(collectionName));
            }

            this.ProjectId = projectId;
            this.CollectionName = collectionName;
        }

        /// <summary>
        ///     Gets the name of the collection.
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; }
    }
}

namespace Surveys.Common.Firestore.Contracts
{
    /// <summary>
    ///     Configuration of the database.
    /// </summary>
    public interface IDatabaseConfiguration
    {
        /// <summary>
        ///     Gets the name of the collection.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }
    }
}

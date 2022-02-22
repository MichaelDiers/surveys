namespace Surveys.Common.Firestore.Contracts
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task InsertAsync(string documentId, IDictionaryConverter data);

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task InsertAsync(IDictionaryConverter data);
    }
}

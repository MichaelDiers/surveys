namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Describes the base for models.
    /// </summary>
    public interface IBase : IDictionaryConverter
    {
        /// <summary>
        ///     Gets the id.
        /// </summary>
        string Id { get; }
    }
}

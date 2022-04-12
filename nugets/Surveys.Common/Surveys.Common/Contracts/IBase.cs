namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes the base for models.
    /// </summary>
    public interface IBase : IToDictionary
    {
        /// <summary>
        ///     Gets the id.
        /// </summary>
        string Id { get; }
    }
}

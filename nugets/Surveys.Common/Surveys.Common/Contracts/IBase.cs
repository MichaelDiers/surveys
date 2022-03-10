namespace Surveys.Common.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

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

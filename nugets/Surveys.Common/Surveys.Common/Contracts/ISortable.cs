namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Indicates that instances are sortable.
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        ///     Gets the sorting order.
        /// </summary>
        int Order { get; }
    }
}

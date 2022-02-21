namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Base definition of pub/sub messages.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///     Gets the process id.
        /// </summary>
        string ProcessId { get; }
    }
}

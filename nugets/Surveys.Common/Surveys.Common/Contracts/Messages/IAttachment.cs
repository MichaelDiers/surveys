namespace Surveys.Common.Contracts.Messages
{
    /// <summary>
    ///     Describes a mail attachment.
    /// </summary>
    public interface IAttachment
    {
        byte[] Data { get; }
        string Name { get; }
    }
}

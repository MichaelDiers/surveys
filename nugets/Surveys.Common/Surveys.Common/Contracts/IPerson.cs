namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Describes a survey organizer and is base class for participants.
    /// </summary>
    public interface IPerson : IBase
    {
        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        string Email { get; }

        /// <summary>
        ///     Gets the name of the person.
        /// </summary>
        string Name { get; }
    }
}

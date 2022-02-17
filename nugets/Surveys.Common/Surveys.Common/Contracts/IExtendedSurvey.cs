namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes the data of a survey.
    /// </summary>
    public interface ISurvey : IBase
    {
        /// <summary>
        ///     Gets an info text for the survey.
        /// </summary>
        string Info { get; }

        /// <summary>
        ///     Gets a link that provides additional survey info.
        /// </summary>
        string Link { get; }

        /// <summary>
        ///     Gets the name of the survey.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the organizer of the survey.
        /// </summary>
        IPerson Organizer { get; }

        /// <summary>
        ///     Gets the participants of the survey.
        /// </summary>
        IEnumerable<IParticipant> Participants { get; }

        /// <summary>
        ///     Gets the questions of the survey.
        /// </summary>
        IEnumerable<IQuestion> Questions { get; }
    }
}

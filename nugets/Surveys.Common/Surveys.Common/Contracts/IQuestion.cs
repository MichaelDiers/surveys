namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes a question of a survey.
    /// </summary>
    public interface IQuestion : IBase
    {
        /// <summary>
        ///     Gets the choices of the question.
        /// </summary>
        IEnumerable<IChoice> Choices { get; }

        /// <summary>
        ///     Gets the order of the question used for sorting.
        /// </summary>
        int Order { get; }

        /// <summary>
        ///     Gets the text of the question.
        /// </summary>
        string Text { get; }
    }
}

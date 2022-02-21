namespace Surveys.Common.Contracts
{
    /// <summary>
    ///     Describe the choice of a question.
    /// </summary>
    public interface IChoice : IBase, ISortable
    {
        /// <summary>
        ///     Gets the answer of a question.
        /// </summary>
        string Answer { get; }

        /// <summary>
        ///     Indicates if the answer is a valid answer or an info text.
        /// </summary>
        bool Selectable { get; }
    }
}

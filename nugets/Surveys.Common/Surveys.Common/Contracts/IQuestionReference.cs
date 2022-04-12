namespace Surveys.Common.Contracts
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes a suggested answer of a survey question.
    /// </summary>
    public interface IQuestionReference : IToDictionary
    {
        /// <summary>
        ///     Gets the id of the choice.
        /// </summary>
        string ChoiceId { get; }

        /// <summary>
        ///     Gets the id of the question.
        /// </summary>
        string QuestionId { get; }
    }
}

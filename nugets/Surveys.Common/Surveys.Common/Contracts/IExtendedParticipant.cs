namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes a participant of a survey.
    /// </summary>
    public interface IParticipant : IPerson
    {
        /// <summary>
        ///     Gets the suggested answers of survey questions.
        /// </summary>
        IEnumerable<IQuestionReference> QuestionReferences { get; }
    }
}
